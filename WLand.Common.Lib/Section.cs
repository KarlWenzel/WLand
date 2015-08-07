using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLand.Common.Lib
{
  public class Section : WBaseModel
  {
    public int ID { get; set; }

    private int sec;
    public int Sec
    {
      get { return this.sec; }
      set
      {
        if (value < 1 || value > 36)
        {
          this.SetErrors(() => this.Sec, String.Format("{0} must be between {1} and {2}.", value, 1, 36));
        }
        else
        {
          this.ClearErrors(() => this.Sec);
        }

        SetProperty(ref this.sec, value);
      }
    }

    private int twn;
    public int Twn
    {
      get { return this.twn; }
      set
      {
        if (value < 1)
        {
          this.SetErrors(() => this.Twn, String.Format("{0} must be greater than zero."));
        }
        else
        {
          this.ClearErrors(() => this.Twn);
        }

        SetProperty(ref this.twn, value);
      }
    }

    private string twnDir;
    public string TwnDir
    {
      get { return this.twnDir; }
      set
      {
        string newTwnDir = String.IsNullOrWhiteSpace(value) ? "" : value.Substring(0,1).ToUpper();

        if (!newTwnDir.Equals("N") && !newTwnDir.Equals("S"))
        {
          this.SetErrors(() => this.TwnDir, "Must be 'N' or 'S'");
        }
        else
        {
          this.ClearErrors(() => this.TwnDir);
        }

        SetProperty(ref this.twnDir, newTwnDir);
      }
    }

    private int rng;
    public int Rng
    {
      get { return this.rng; }
      set
      {
        if (value < 1)
        {
          this.SetErrors(() => this.Rng, String.Format("{0} must be greater than zero."));
        }
        else
        {
          this.ClearErrors(() => this.Rng);
        }

        SetProperty(ref this.rng, value);
      }
    }

    private string rngDir;
    public string RngDir
    {
      get { return this.rngDir; }
      set
      {
        string newRngDir = String.IsNullOrWhiteSpace(value) ? "" : value.Substring(0, 1).ToUpper();

        if (!newRngDir.Equals("E") && !newRngDir.Equals("W"))
        {
          this.SetErrors(() => this.RngDir, "Must be 'E' or 'W'");
        }
        else
        {
          this.ClearErrors(() => this.RngDir);
        }

        SetProperty(ref this.rngDir, newRngDir);
      }
    }

    private string meridian;
    public string Meridian
    {
      get { return this.meridian; }
      set
      {
        if (String.IsNullOrWhiteSpace(value))
        {
          this.SetErrors(() => this.Meridian, "Meridian required");
        }
        else
        {
          this.ClearErrors(() => this.Meridian);
        }

        SetProperty(ref this.meridian, value);
      }
    }

    private string stateAbbrev;
    public string StateAbbrev
    {
      get { return this.stateAbbrev; }
      set
      {
        if (String.IsNullOrWhiteSpace(value))
        {
          this.SetErrors(() => this.StateAbbrev, "State required");
        }
        else
        {
          this.ClearErrors(() => this.StateAbbrev);
        }

        SetProperty(ref this.stateAbbrev, value);
      }
    }

    private List<string> counties = new List<string>();
    public List<string> Counties
    {
      get { return this.counties; }
      set
      {
        var input = (value == null) 
          ? new List<string>() 
          : value.Where(x => !String.IsNullOrWhiteSpace(x)).Distinct().ToList();

        if (input.Count < 1)
        {
          this.SetErrors(() => this.Counties, "Counties Required");
        }
        else if (input.Count > 4)
        {
          this.SetErrors(() => this.Counties, "More than four Counties assigned.  Assuming this is a mistake.");
        }
        else
        {
          this.ClearErrors(() => this.Counties);
        }

        SetProperty(ref this.counties, input);
      }
    }
  }
}
