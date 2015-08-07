using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WLand.Modules.Json.BO;
using WLand.Modules.Json.Interfaces;

namespace WLand.Modules.Json
{
  [Export(typeof(IJsonTest))]
  public partial class JsonTest : UserControl, IJsonTest
  {
    public JsonTest()
    {
      InitializeComponent();
      this.btnClick.Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
      var ds = DataStore.GetTestData();

      var settings = new JsonSerializerSettings 
      { 
        PreserveReferencesHandling = PreserveReferencesHandling.Objects 
      };

      string output = JsonConvert.SerializeObject(ds, Formatting.Indented, settings);
      var ds2 = JsonConvert.DeserializeObject<DataStore>(output);
    }
  }
}
