using Microsoft.Practices.Prism.Mvvm;
using MvvmValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WLand.Modules.Json.BO
{
  public enum ValidationModes
  {
    Default,
    New
  }

  public class BaseModel : BindableBase, IValidatable, IDataErrorInfo
  {
    public BaseModel() : base() 
    {     
      this.Validator = new ValidationHelper();
      Validators = new Dictionary<ValidationModes, ValidationHelper>();

      HookUpValidationNotification();
    }

    public bool Dirty { get; set; }

    public virtual bool HasErrors { get { return !Validator.GetResult().IsValid; } }

    public Dictionary<ValidationModes, ValidationHelper> Validators { get; protected set; }

    public ValidationModes ValidationMode { set { Validator = Validators[value]; } }

    protected ValidationHelper _Validator;
    public ValidationHelper Validator
    {
      get { return this._Validator; }
      set
      {
        this._Validator = value;
        this.NotifyDataErrorInfoAdapter = new DataErrorInfoAdapter(value);
      }
    }

    public event EventHandler<ValidationResultChangedEventArgs> ValidationResultChanged
    {
      add
      {
        Validator.ResultChanged += value;
      }
      remove
      {
        Validator.ResultChanged -= value;
      }
    }
    
    /// <summary>
    ///  Adapts local Validator property (a ValidationHelper) to the see the INotifyDataErrorInfo interface.
    /// </summary>
    private DataErrorInfoAdapter NotifyDataErrorInfoAdapter { get; set; }

    public virtual string this[string columnName]
    {
      get { return NotifyDataErrorInfoAdapter[columnName]; }
    }

    public virtual string Error
    {
      get { return NotifyDataErrorInfoAdapter.Error; }
    }

    private void HookUpValidationNotification()
    {
      // Due to limitation of IDataErrorInfo, in WPF we need to explicitly indicated that something has changed
      // about the property in order for the framework to look for errors for the property.
      Validator.ResultChanged += (o, e) =>
      {
        var propertyName = e.Target as string;

        if (!string.IsNullOrEmpty(propertyName))
        {
          OnPropertyChanged(propertyName);
        }
      };
    }

    /// <summary>
    /// Validates entire object using INotifyDataErrorInfo logic.  Use this as a wrapper method for each of the class's individual property validation methods.
    /// </summary>
    public void ValidateAll()
    {
      this.Validator.ValidateAll();
    }

    public void ValidateAllAsync()
    {
      this.Validator.ValidateAllAsync();
    }

    /// <summary>
    /// Returns null if no validation occurred.
    /// </summary>
    /// <param name="property"></param>
    /// <param name="cancelIfDirty"></param>
    /// <returns></returns>
    public MvvmValidation.ValidationResult Validate(Expression<Func<object>> property, bool cancelIfDirty = true)
    {
      if (!this.Dirty || !cancelIfDirty)
      {
        return this.Validator.Validate(property);
      }
      return null;
    }

    Task<MvvmValidation.ValidationResult> IValidatable.Validate()
    {
      return Validator.ValidateAllAsync();
    }


  }
}
