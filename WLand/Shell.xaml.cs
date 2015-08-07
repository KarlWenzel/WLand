using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
using System.Windows.Shapes;
using WLand.Common.Inf;

namespace WLand
{
  /// <summary>
  /// Interaction logic for Shell.xaml
  /// </summary>
  [Export]
  public partial class Shell : Window
  {
    public Shell()
    {
      InitializeComponent();

      this.btnNewTab.Click += OnNewTab;
    }

    [Import]
    public ShellVM ViewModel 
    {
      get { return this.DataContext as ShellVM; }
      set { this.DataContext = value; } 
    }

    protected void OnNewTab(object sender, RoutedEventArgs e)
    {
      var keyValPair = (KeyValuePair<string, Type>)this.cboNetTabTypes.SelectedItem;
      this.ViewModel.AddTab(keyValPair.Key);
    }

    //protected void OnNewTab(object sender, RoutedEventArgs e)
    //{
    //  var lblTabName = new Label();
    //  lblTabName.Content = "HI";

    //  var ctrl = new ContentControl();
      
    //  var newTab = new TabItem();
    //  newTab.Header = lblTabName;

    //  this.tabMain.Items.Add(newTab);
    //}
  }
}
