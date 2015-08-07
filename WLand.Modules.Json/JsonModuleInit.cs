using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLand.Common.Inf;

namespace WLand.Modules.Json
{
  [ModuleExport(typeof(WLand.Modules.Json.JsonModuleInit))]
  public class JsonModuleInit : IModule
  {
    private readonly ILoggerFacade logger;
    private readonly IRegionManager regionManager;

    [ImportingConstructor]
    public JsonModuleInit(ILoggerFacade logger, IRegionManager regionManager)
    {
      this.logger = logger;
      this.regionManager = regionManager;
    }

    public void Initialize()
    {

    }
  }
}
