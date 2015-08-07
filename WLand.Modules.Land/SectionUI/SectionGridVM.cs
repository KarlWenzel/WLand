using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WLand.Common.Inf;
using WLand.Common.Lib;

namespace WLand.Modules.Land.SectionUI
{
  [Export]
  public class SectionGridVM : WBaseModel
  {
    private readonly IEventAggregator eventAggregator;
    
    public ICollectionView Sections { get; private set; }

    [ImportingConstructor]
    public SectionGridVM(IEventAggregator eventAggregator)
    {
      this.eventAggregator = eventAggregator;
      this.Sections = new ListCollectionView(Cache.Sections);
    }
  }
}
