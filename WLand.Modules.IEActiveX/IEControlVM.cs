using SHDocVw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WLand.Common.Lib;

namespace WLand.Modules.IEActiveX
{
  public class IEControlVM : WBaseModel
  {
    /*
     * Note that we have the benefit of accessing the ActiveX object that represents the 
     * System.Window.Controls.WebBrowser that .NET gives us.  We had to do some work to obtain
     * the SHDocVM namespace however: 
     * (1) find desired system's ieframe.dll... e.g. C:\Windows\System32\ieframe.dll
     * (2) convert it to SHDocVw.dll using the TlbImp.exe utilty provided by VS
     * (3) reference the local SHDocVw.dll file from the desired Projects
     * Bibliography: http://stackoverflow.com/a/18269105/470679
     */
    private SHDocVw.WebBrowser ieActiveX;

    public SHDocVw.WebBrowser IEActiveX
    {
      get { return this.ieActiveX; }
      set { SetProperty(ref this.ieActiveX, value); }
    }

    private System.Windows.Controls.WebBrowser webBrowser;

    public System.Windows.Controls.WebBrowser WebBrowser
    {
      get { return this.webBrowser; }
      set
      {
        SetProperty(ref this.webBrowser, value);
        this.webBrowser.Loaded += OnWebBrowserLoaded;
      }
    }

    protected void OnWebBrowserLoaded(object sender, RoutedEventArgs e)
    {
      this.IEActiveX = this.webBrowser.GetType().InvokeMember("ActiveXInstance",
               BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
               null, this.webBrowser, new object[] { }) as SHDocVw.WebBrowser;

      // now we can handle previously inaccessible System.Windows.Controls.WebBrowser events 
      // this.IEActiveX.FileDownload += activeX_FileDownload;
    }

    private string url;

    public string Url
    {
      get { return this.url; }
      set
      {
        SetProperty(ref this.url, value);
      }
    }

    public void OnNavigateToURL(object sender, RoutedEventArgs e)
    {
      this.WebBrowser.Navigate(this.Url);
    }

    public IEControlVM(System.Windows.Controls.WebBrowser webBrowser)
    {
      this.WebBrowser = webBrowser;
      this.WebBrowser.Navigated += (s, e) => 
      {
        this.Url = (e.Uri == null) ? null : e.Uri.ToString();
      };
    }
  }
}
