using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.Wpf;
using WLand.Common.Lib;
using System.ComponentModel;

namespace WLand.Modules.Browser
{
  [Export]
  public class BrowserControlVM : WBaseModel
  {
    public const string DEFAULT_URL = "https://www.google.com/#safe=off&q=cats";

    private string Address
    {
      get { return this.WebBrowser.Address; }
      set
      {
        var chrome = this.WebBrowser as ChromiumWebBrowser;
        chrome.Load(value);
        OnPropertyChanged(() => this.Address);
      }
    }

    private IWpfWebBrowser webBrowser;

    public IWpfWebBrowser WebBrowser
    {
      get { return this.webBrowser; }
      set 
      { 
        SetProperty(ref this.webBrowser, value);
      }
    }

    private string title;

    public string Title
    {
      get { return this.title; }
      set { SetProperty(ref this.title, value); }
    }


    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {

    }

  }
}
