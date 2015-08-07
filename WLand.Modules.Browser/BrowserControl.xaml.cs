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
using WLand.Modules.Browser.Interfaces;
using CefSharp.Wpf;
using CefSharp;
using WLand.Modules.HtmlParser;

namespace WLand.Modules.Browser
{
  [Export(typeof(IBrowserControl))]
  public partial class BrowserControl : UserControl, IBrowserControl
  {
    public BrowserControl()
    {
      InitializeComponent();
      //Cef.Initialize(new CefSettings());
    }

    [Import]
    public BrowserControlVM ViewModel
    {
      get { return this.DataContext as BrowserControlVM; }
      set { this.DataContext = value; }
    }
    
    private void btnGo_Click(object sender, RoutedEventArgs e)
    {
      this.ViewModel.WebBrowser.Load(this.txtUrl.Text);
    }

    private async void btnScrape_Click(object sender, RoutedEventArgs e)
    {
      string html = await this.ViewModel.WebBrowser.GetSourceAsync();
      var rec = OkCountyRecParser.FromHtml(html);
      int i = rec.Count();
    }
  }
}
