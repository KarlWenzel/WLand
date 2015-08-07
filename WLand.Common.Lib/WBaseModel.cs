using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WLand.Common.Lib
{
  public class WBaseModel : BindableBase, INotifyDataErrorInfo
  {
    /*
     * This is basically an adapter class that implements the INotfiyDataErrorInfo interface on a BindableBase,
     * and this is accomblished using a protected ErrorsContainer<T> property, a public ErrorsChanged event,
     * and a variety of public wrapper methods for getting and setting error-related information.
     */

    protected WBaseModel() { }

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public bool HasErrors { get { return this.ErrorsContainer.HasErrors; } }

    private ErrorsContainer<string> errorsContainer;
    protected ErrorsContainer<string> ErrorsContainer
    {
      get
      {
        if (this.errorsContainer == null)
        {
          this.errorsContainer = new ErrorsContainer<string>(pn => this.RaiseErrorsChanged(pn));
        }
        return this.errorsContainer;
      }
    }

    protected void RaiseErrorsChanged(string propertyName)
    {
      this.OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
    }

    protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
    {
      var handler = this.ErrorsChanged;
      if (handler != null)
      {
        handler(this, e);
        OnPropertyChanged(() => this.HasErrors);
      }
    }

    public void ClearErrors(string propertyName)
    {
      this.ErrorsContainer.ClearErrors(propertyName);
    }

    public void ClearErrors<T>(Expression<Func<T>> propertyExpression)
    {
      var propertyName = Microsoft.Practices.Prism.Mvvm.PropertySupport.ExtractPropertyName(propertyExpression);
      this.ClearErrors(propertyName);
    }

    public IEnumerable GetErrors(string propertyName)
    {
      return this.ErrorsContainer.GetErrors(propertyName);
    }

    public IEnumerable GetErrors<T>(Expression<Func<T>> propertyExpression)
    {
      var propertyName = Microsoft.Practices.Prism.Mvvm.PropertySupport.ExtractPropertyName(propertyExpression);
      return this.GetErrors(propertyName);
    }

    public void SetErrors(string propertyName, string error)
    {
      this.ErrorsContainer.SetErrors(propertyName, new string[] { error });
    }

    public void SetErrors(string propertyName, IEnumerable<string> errors)
    {
      this.ErrorsContainer.SetErrors(propertyName, errors);
    }

    public void SetErrors<T>(Expression<Func<T>> propertyExpression, string error)
    {
      var propertyName = Microsoft.Practices.Prism.Mvvm.PropertySupport.ExtractPropertyName(propertyExpression);
      this.SetErrors(propertyName, error);
    }

    public void SetErrors<T>(Expression<Func<T>> propertyExpression, IEnumerable<string> errors)
    {
      var propertyName = Microsoft.Practices.Prism.Mvvm.PropertySupport.ExtractPropertyName(propertyExpression);
      this.SetErrors(propertyName, errors);
    }
  }
}
