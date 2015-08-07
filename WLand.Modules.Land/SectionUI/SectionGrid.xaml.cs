using System;
using System.Collections.Generic;
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
using WLand.Common.Inf;
using WLand;
using WLand.Modules.Land.Interfaces;

namespace WLand.Modules.Land.SectionUI
{
  [Export(typeof(ISectionGrid))]
  public partial class SectionGrid : UserControl, ISectionGrid
  {
    public SectionGrid()
    {
      InitializeComponent();
    }

    [Import]
    public SectionGridVM ViewModel
    {
      get { return this.DataContext as SectionGridVM; }
      set { this.DataContext = value; }
    }
  }
}
