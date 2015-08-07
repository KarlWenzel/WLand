using MvvmValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WLand.Common.Lib;

namespace WLand.Modules.Json.BO
{
  public class LandUnit : BaseModel
  {
    public LandUnit() : base() 
    {
      SetValidators();
    }
    
    private decimal grossAc;
    public decimal GrossAc
    {
      get { return this.grossAc; }
      set { 
        SetProperty(ref this.grossAc, value);
        Validate(() => this.GrossAc);
      }
    }

    public string FormattedUnitDescription
    {
      get
      {
        if (!Validator.GetResult(() => this.Sec).IsValid || !Validator.GetResult(() => this.Twn).IsValid || !Validator.GetResult(() => this.Rng).IsValid)
        {
          return "Invalid Description";
        }
        else
        {
          return String.Format("{0}-T{1}-R{2}", this.Sec.ToString().PadLeft(2, '0'), this.Twn.ToString().PadLeft(3, '0'), this.Rng.ToString().PadLeft(3, '0'));
        }
      }
    }

    private int? sec;
    public int? Sec
    {
      get { return this.sec; }
      set
      {
        SetProperty(ref this.sec, value);
        Validate(() => this.Sec);
        OnPropertyChanged(() => this.FormattedUnitDescription);
      }
    }

    private string twn;
    public string Twn
    {
      get { return this.twn; }
      set
      {
        SetProperty(ref this.twn, value);
        Validate(() => this.Twn);
        OnPropertyChanged(() => this.FormattedUnitDescription);
      }
    }

    private string rng;
    public string Rng
    {
      get { return this.rng; }
      set
      {
        SetProperty(ref this.rng, value);
        Validate(() => this.Rng);
        OnPropertyChanged(() => this.FormattedUnitDescription);
      }
    }
    
    private string meridian;
    public string Meridian
    {
      get { return this.meridian; }
      set
      {
        SetProperty(ref this.meridian, value);
        OnPropertyChanged(() => this.FormattedUnitDescription);
      }
    }

    #region Validation Rules
    public void ResetValidators()
    {
      Validator = new ValidationHelper();
      SetValidators();
      ValidateAll();
    }

    private void SetValidators()
    {


      Validator.AddRule(() => GrossAc, () => RuleResult.Assert(GrossAc > 0M, "Gross Acres must be greater than 0!"));

      Validator.AddRule(() => Sec,
                         () => RuleResult.Assert(Sec >= 1 && Sec <= 36, "Sec must be a number between 1 - 36!"));


      Validator.AddRule(() => Twn,
                        () =>
                        {
                          if (!string.IsNullOrEmpty(Twn))
                          {
                            const string regexPattern =
                              @"^\d{1,2}[NS]$";

                            var match = Regex.IsMatch(Twn, regexPattern);
                            return RuleResult.Assert(match,
                                                     "Township must be a one or two digit number and must end in 'N' or 'S'");
                          }
                          return RuleResult.Valid();

                        });

      Validator.AddRule(() => Rng,
                         () =>
                         {
                           if (!string.IsNullOrEmpty(Rng))
                           {
                             const string regexPattern =
                               @"^\d{1,2}[EW]$";
                             return RuleResult.Assert(Regex.IsMatch(Rng, regexPattern),
                                                      "Range must be a one or two digit number and must end in 'E' or 'W'");
                           }
                           return RuleResult.Valid();

                         });
    }
    #endregion Validation Rules
  }
}
