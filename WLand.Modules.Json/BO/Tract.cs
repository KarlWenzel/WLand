using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLand.Common.Lib;

namespace WLand.Modules.Json.BO
{
  public class Tract : WBaseModel
  {
    public Tract() : base() { }

    public DataStore DataStore { get; set; }

    public int ID
    {
      get
      {
        return (this.DataStore == null) ? -1 : this.DataStore.Tracts.IndexOf(this);
      }
    }

    private string legal;
    public string Legal
    {
      get { return this.legal; }
      set { SetProperty(ref this.legal, value); }
    }

    private decimal grossAc;
    public decimal GrossAc
    {
      get { return this.grossAc; }
      set { SetProperty(ref this.grossAc, value); }
    }

    private string notes;
    public string Notes
    {
      get { return this.notes; }
      set { SetProperty(ref this.notes, value); }
    }
  }
}
