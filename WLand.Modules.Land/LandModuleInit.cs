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

namespace WLand.Modules.Land
{
  [ModuleExport(typeof(WLand.Modules.Land.LandModuleInit))]
  public class LandModuleInit : IModule
  {
    private readonly ILoggerFacade logger;
    private readonly IRegionManager regionManager;

    [ImportingConstructor]
    public LandModuleInit(ILoggerFacade logger, IRegionManager regionManager)
    {
      this.logger = logger;
      this.regionManager = regionManager;
    }

    public void Initialize()
    {
      //this.regionManager.RegisterViewWithRegion(RegionNames.LEASE_CONVEY_REGION, typeof(LeaseUI.LeaseConvey));
      //this.regionManager.RegisterViewWithRegion(RegionNames.SECTION_REGION, typeof(SectionUI.SectionGrid));

      /*
       * THIS WORKS
       * 
      IRegion secRegion = this.regionManager.Regions[RegionNames.SECTION_REGION];
      secRegion.RequestNavigate(new Uri("SectionGrid", UriKind.Relative));
       * 
       */

    }
  }
}
