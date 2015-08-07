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
using WLand.Modules.IEActiveX.Interfaces;
using SHDocVw;
using System.Reflection;

namespace WLand.Modules.IEActiveX
{
  [Export(typeof(I_IEActiveX))]
  public partial class IEControl : UserControl, I_IEActiveX
  {
    private SHDocVw.WebBrowser activeX;

    public IEControlVM ViewModel 
    { 
      get { return this.DataContext as IEControlVM; }
      set { this.DataContext = value; }
    }

    public IEControl()
    {
      InitializeComponent();

      this.ViewModel = new IEControlVM(this.WB);
      this.ButtonGo.Click += this.ViewModel.OnNavigateToURL;      
      this.Loaded += (s, e) => { this.WB.Navigate("http://google.com"); };
    }
  }
}
