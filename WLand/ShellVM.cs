using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using WLand.Modules.Land.Interfaces;
using WLand.Common.Inf;
using System;
using System.Collections.Generic;
using WLand.Controls;
using WLand.Modules.Browser.Interfaces;
using WLand.Modules.IEActiveX.Interfaces;
using WLand.Modules.Json.Interfaces;

namespace WLand
{
  [Export]
  public class ShellVM : BindableBase
  {
    private readonly IServiceLocator serviceLocator;
    private readonly IEventAggregator eventAggregator;

    public ObservableCollection<MyTabItem> Tabs { get; set; }

    public Dictionary<string, Type> TabViewTypes
    {
      get
      {
        return new Dictionary<string, Type>() {
          { "Json", typeof(IJsonTest)},
          { "Scraper", typeof(WebScraperControl) },
          { "IE", typeof(I_IEActiveX) },
          { "Chrome", typeof(IBrowserControl) },
          { "Sections", typeof(ISectionGrid) },
          { "Lease Conveys", typeof(ILeaseConvey) }
        };
      }
    }

    [ImportingConstructor]
    public ShellVM(IServiceLocator serviceLocator, IEventAggregator eventAggregator)
    {
      this.serviceLocator = serviceLocator;
      this.eventAggregator = eventAggregator;

      this.Tabs = new ObservableCollection<MyTabItem>();
    }

    public void AddTab(string name)
    {
      Type viewType = this.TabViewTypes[name];
      var tabItem = new MyTabItem()
      {
        Header = name,
        Content = this.serviceLocator.GetInstance(viewType) as UserControl
      };
      this.Tabs.Add(tabItem);
    }
  }

  public class MyTabItem : BindableBase
  {
    public string Header { get; set; }
    public UserControl Content { get; set; }
  }
}
