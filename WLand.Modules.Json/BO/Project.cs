using MvvmValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLand.Common.Lib;

namespace WLand.Modules.Json.BO
{
  public class Project : BaseModel
  {
    public Project() : base() 
    {
      this.Unit = new LandUnit();

      Validator.AddRule(() => Unit,
                         () => RuleResult.Assert( !Unit.HasErrors, "LandUnit has errors" ));
    }

    public Project(string projectName) : this()
    { 
      this._ProjectName = projectName;
    }

    private string _ProjectName;
    public string ProjectName
    {
      get { return this._ProjectName; }
      set { 
        SetProperty(ref this._ProjectName, value);
        Validate(() => this.ProjectName);
        OnPropertyChanged(() => this.ProjectFileName);
      }
    }    
    
    public string ProjectFileName { get { return String.Format("{0}.json", this.ProjectName); } }

    public string ProjectFilePath(string projectRoot)
    {
      return string.Format("{0}\\{1}\\{2}", projectRoot, this.ProjectName, this.ProjectFileName);
    }

    private LandUnit _Unit;
    public LandUnit Unit
    {
      get { return this._Unit; }
      private set
      {
        SetProperty(ref this._Unit, value);
        if (this._Unit != null)
        {
          this._Unit.ValidationResultChanged += Unit_ValidationResultChanged;
        }
      }
    }
    
    private string _State;
    public string State
    {
      get { return this._State; }
      set
      {
        SetProperty(ref this._State, value);
        Validate(() => this.State);
      }
    }
        
    private string _County;
    public string County
    {
      get { return this._County; }
      set
      {
        SetProperty(ref this._County, value);
        Validate(() => this.County);
      }
    }

    
    private string _Prospect;
    public string Prospect
    {
      get { return this._Prospect; }
      set
      {
        SetProperty(ref this._Prospect, value);
        Validate(() => this.Prospect);
      }
    }

    private DateTime? _CertificationDate;
    public DateTime? CertificationDate
    {
      get { return this._CertificationDate; }
      set { 
        SetProperty(ref this._CertificationDate, value);
        Validate(() => this.CertificationDate);
      }
    }

    private string _CertificationBook;
    public string CertificationBook
    {
      get { return this._CertificationBook; }
      set { 
        SetProperty(ref this._CertificationBook, value);
        Validate(() => this.CertificationBook);
      }
    }

    private string _CertificationPage;
    public string CertificationPage
    {
      get { return this._CertificationPage; }
      set { 
        SetProperty(ref this._CertificationPage, value);
        Validate(() => this.CertificationPage);
      }
    }

    #region Validation Rules

    private void SetValidators()
    {
      Validator.AddRule(() => Unit, () =>
      {
        if (Unit.HasErrors)
        {
          return RuleResult.Invalid(Unit.Error);
        }
        return RuleResult.Valid();
      });
    }

    public void ResetValidators()
    {
      this.Validator = new ValidationHelper();
      SetValidators();

      Unit.ResetValidators();
      Unit.ValidationResultChanged += Unit_ValidationResultChanged;

      ValidateAll();
    }

    public void Unit_ValidationResultChanged(object sender, ValidationResultChangedEventArgs e)
    {
      Validate(() => Unit);
    }

    #endregion
  }
}
