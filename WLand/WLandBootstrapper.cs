using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using WLand.Modules.Land;
using WLand.Modules.Browser;

/*
 * OK BRAH
 * 
 * Wrap Web Browser object in a UserControl with interface that exposes
 * 1.  Go to URL
 * 2.  Get HTML as string
 * 3.  Run Javascript (with or without JSON result as string)
 * 4.  Expose collection property of stored URLs as string
 * 
 * Define class(es) that take strings and parse them into HTML DOMs, as well as deserialize JSON strings into POCOs.
 * 1.  HtmlTo
 */

namespace WLand
{
  public class WLandBootstrapper : MefBootstrapper
  {
    private readonly EnterpriseLibraryLoggerAdapter logger = new EnterpriseLibraryLoggerAdapter();

    protected override ILoggerFacade CreateLogger()
    {
      return this.logger;
    }

    protected override void ConfigureAggregateCatalog()
    {
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(WLandBootstrapper).Assembly));
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(WLand.Modules.Browser.BrowserModuleInit).Assembly));
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(WLand.Modules.IEActiveX.IEActiveXModuleInit).Assembly));
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(WLand.Modules.Land.LandModuleInit).Assembly));
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(WLand.Modules.HtmlParser.HtmlParserModuleInit).Assembly));
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(WLand.Modules.Json.JsonModuleInit).Assembly));
    }

    protected override void ConfigureContainer()
    {
      base.ConfigureContainer();
    }

    protected override void InitializeShell()
    {
      base.InitializeShell();

      Application.Current.MainWindow = (Shell)this.Shell;
      Application.Current.MainWindow.Show();
    }

    protected override DependencyObject CreateShell()
    {
      return this.Container.GetExportedValue<Shell>();
    }


  }
}
