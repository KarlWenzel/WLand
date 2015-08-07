using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WLand.Modules.Land.Interfaces;

namespace WLand.Modules.Land.LeaseUI
{
  public class ExampleLeaseVM
  {
    public ObservableCollection<ExampleTract> Tracts { get; set; }
    public ObservableCollection<ExampleOwner> Owners { get; set; }
    public ObservableCollection<ExampleTractOwner> TractOwners { get; set; }

    public ObservableCollection<ExampleOwner> Lessors { get; set; }
    public ObservableCollection<ExampleOwner> Lessees { get; set; }
    public ObservableCollection<ExampleTract> LeaseTracts { get; set; }    

    public ExampleLeaseVM()
    {
      this.Owners = new ObservableCollection<ExampleOwner>();
      this.Lessees = new ObservableCollection<ExampleOwner>();
      this.LeaseTracts = new ObservableCollection<ExampleTract>();
      this.Lessors = new ObservableCollection<ExampleOwner>();
      this.Tracts = new ObservableCollection<ExampleTract>();

      this.Owners.Add(new ExampleOwner() { OwnerName = "Owner 1" });
      this.Owners.Add(new ExampleOwner() { OwnerName = "Owner 2" });
      this.Owners.Add(new ExampleOwner() { OwnerName = "Owner 3" });
      this.Owners.Add(new ExampleOwner() { OwnerName = "Owner 4" });
      this.Owners.Add(new ExampleOwner() { OwnerName = "Owner 5" });

      this.Tracts.Add(new ExampleTract() { TractLabel = "Tract 1" });
      this.Tracts.Add(new ExampleTract() { TractLabel = "Tract 2" });

      this.Lessors.Add(this.Owners[0]);
      this.Lessors.Add(this.Owners[1]);
      this.Lessees.Add(this.Owners[4]);
      this.LeaseTracts.Add(this.Tracts[0]);

      var t = this.Tracts[0];
      t.Owners.Add(new ExampleTractOwner() { Owner = this.Owners[0], MI = 0.5 });
      t.Owners.Add(new ExampleTractOwner() { Owner = this.Owners[1], MI = 0.5 });
      t.DivestedWI.Add(new ExampleDivestedWI() { Owner = this.Owners[0], ConveyType = "Rel To Tract", Amount = 0.5 });
      t.DivestedWI.Add(new ExampleDivestedWI() { Owner = this.Owners[1], ConveyType = "Rel To Tract", Amount = 0.5 });
      t.AcquiredWI.Add(new ExampleAcquiredWI() { Owner = this.Owners[4], Amount = 1.0 });

      t = this.Tracts[1];
      t.Owners.Add(new ExampleTractOwner() { Owner = this.Owners[0], MI = 0.25 });
      t.Owners.Add(new ExampleTractOwner() { Owner = this.Owners[1], MI = 0.25 });
      t.Owners.Add(new ExampleTractOwner() { Owner = this.Owners[2], MI = 0.5 });

    }
  }

  public class ExampleTract
  {
    public string TractLabel { get; set; }
    public ObservableCollection<ExampleTractOwner> Owners { get; set; }
    public ObservableCollection<ExampleDivestedWI> DivestedWI { get; set; }
    public ObservableCollection<ExampleAcquiredWI> AcquiredWI { get; set; }

    public ExampleTract()
    {
      this.Owners = new ObservableCollection<ExampleTractOwner>();
      this.DivestedWI = new ObservableCollection<ExampleDivestedWI>();
      this.AcquiredWI = new ObservableCollection<ExampleAcquiredWI>();
    }
  }

  public class ExampleOwner
  {
    public string OwnerName { get; set; }
  }

  public class ExampleTractOwner
  {
    public ExampleOwner Owner { get; set; }
    public double MI { get; set; }
  }

  public class ExampleDivestedWI
  {
    public ExampleOwner Owner { get; set; }
    public string ConveyType { get; set; }
    public double Amount { get; set; }
  }

  public class ExampleAcquiredWI
  {
    public ExampleOwner Owner { get; set; }
    public double Amount { get; set; }
  }

  public class ExampleLessorWI
  {
    public string OwnerName { get; set; }
    public double LeaseInterest { get; set; }
  }

  public class ExampleLessorWIList : List<ExampleLessorWI>
  {
    public ExampleLessorWIList()
    {
      this.Add(new ExampleLessorWI() { OwnerName = "Owner 5", LeaseInterest = 1.0 });
    }
  }

  public class ExampleNri
  {
    public string OwnerName { get; set; }
    public double BaseCalc { get; set; }
    public string BaseCalcFormat { get; set; }
    public string BaseCalcText { get { return String.Format(this.BaseCalcFormat, this.BaseCalc); } }
    public double AdjustCalc { get; set; }
    public double FinalCalc { get { return this.BaseCalc + this.AdjustCalc; } }
  }

  public class ExampleNriList : List<ExampleNri>
  {
    public ExampleNriList()
    {
      this.Add(new ExampleNri() { OwnerName = "Owner 1", AdjustCalc = 0, BaseCalcFormat = "(1/8th) * 0.5 = {0}", BaseCalc = 0.5 / 8 });
      this.Add(new ExampleNri() { OwnerName = "Owner 2", AdjustCalc = 0, BaseCalcFormat = "(1/8th) * 0.5 = {0}", BaseCalc = 0.5 / 8 });
      this.Add(new ExampleNri() { OwnerName = "Owner 5", AdjustCalc = 0, BaseCalcFormat = "(1 - 1/8th) * 0.5 = {0}", BaseCalc = 7D / 8 });
    }
  }

  [Export(typeof(ILeaseConvey))]
  public partial class LeaseConvey : UserControl, ILeaseConvey
  {
    public LeaseConvey()
    {
      InitializeComponent();

      this.DataContext = this.Resources["ExampleLeaseVMData"] as ExampleLeaseVM;
    }

    private void btnAddTract_Click(object sender, RoutedEventArgs e)
    {
      var vm = this.DataContext as ExampleLeaseVM;
      vm.LeaseTracts.Add(vm.Tracts[1]);
    }
  }
}
