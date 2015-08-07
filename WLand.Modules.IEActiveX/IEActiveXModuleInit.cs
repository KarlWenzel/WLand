using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLand.Modules.IEActiveX
{
  [ModuleExport(typeof(WLand.Modules.IEActiveX.IEActiveXModuleInit))]
  public class IEActiveXModuleInit : IModule
  {
    private readonly ILoggerFacade logger;
    private readonly IRegionManager regionManager;

    [ImportingConstructor]
    public IEActiveXModuleInit(ILoggerFacade logger, IRegionManager regionManager)
    {
      this.logger = logger;
      this.regionManager = regionManager;
    }

    public void Initialize()
    {

    }

  }
}
