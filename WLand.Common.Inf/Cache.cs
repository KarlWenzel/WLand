using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLand.Common.Lib;

namespace WLand.Common.Inf
{
  public class Cache
  {
    private static bool LOAD_TEST_DATA = true;

    static Cache()
    {
      if (Cache.LOAD_TEST_DATA)
      {
        Cache.Sections.Add(new Section() { ID = 1, Sec = 1, Twn = 2, TwnDir = "N", Rng = 3, RngDir = "W" });
        Cache.Sections.Add(new Section() { ID = 2, Sec = 2, Twn = 2, TwnDir = "N", Rng = 3, RngDir = "W" });
        Cache.Sections.Add(new Section() { ID = 3, Sec = 3, Twn = 2, TwnDir = "N", Rng = 3, RngDir = "W" });
        Cache.Sections[0].RngDir = "Funkytown";
      }
    }

    private static ObservableCollection<Section> sections = null;
    public static ObservableCollection<Section> Sections
    {
      get
      {
        if (Cache.sections == null)
        {
          Cache.sections = new ObservableCollection<Section>();
        }
        return Cache.sections;
      }
    }


  }
}
