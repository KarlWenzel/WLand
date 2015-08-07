using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLand.Common.Lib;

namespace WLand.Modules.Json.BO
{
  public class DataStore : WBaseModel
  {
    public static DataStore GetTestData()
    {
      var ds = new DataStore();

      var project = new Project()
      {
        ProjectName = "TestProject",
        Prospect = "TestProspect",
        CertificationDate = new DateTime(2015,1,1),
        CertificationBook = "111",
        CertificationPage = "222"
      };
      
      project.Unit.Sec = 1;
      project.Unit.Twn = "2N";
      project.Unit.Rng = "3W";
      project.Unit.Meridian = "IM";
      project.Unit.GrossAc = 640m;

      ds.Project = project;

      var t1 = new Tract() { Legal = "NE4", GrossAc = 160m, Notes = "abc" };
      var t2 = new Tract() { Legal = "NW/4", GrossAc = 160m, Notes = "def" };
      ds.Tracts.Add(t1);
      ds.Tracts.Add(t2);

      return ds;
    }

    public DataStore() : base() 
    {
      this.Tracts = new ObservableCollection<Tract>();
    }

    private Project project;
    public Project Project
    {
      get { return this.project; }
      set { SetProperty(ref this.project, value); }
    }

    private ObservableCollection<Tract> tracts;
    public ObservableCollection<Tract> Tracts
    {
      get { return this.tracts; }
      private set { 
        SetProperty(ref this.tracts, value);
        this.tracts.CollectionChanged += OnTractsCollectionChanged;
      }
    }

    private void OnTractsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems != null)
      {
        foreach (Tract tract in e.NewItems)
        {
          if (tract == null) continue;
          tract.DataStore = this;
        }
      }

    }

  }
}
