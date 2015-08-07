using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLand.Common.Lib
{
  public class OkCountyRecord : WBaseModel
  {
    private  string county;
    public string County
    {
      get { return this.county; }
      set { SetProperty(ref this.county, value); }
    }

    private string instrument;
    public string Instrument
    {
      get { return this.instrument; }
      set { SetProperty(ref this.instrument, value); }
    }

    private string recordType;
    public string RecordType
    {
      get { return this.recordType; }
      set { SetProperty(ref this.recordType, value);  }
    }

    private string book;
    public string Book
    {
      get { return this.book; }
      set { SetProperty(ref this.book, value);  }
    }

    private string pages;
    public string Pages
    {
      get { return this.pages; }
      set { SetProperty(ref this.pages, value);  }
    }

    private ObservableCollection<string> people;
    public ObservableCollection<string> People
    {
      get
      {
        if (this.people == null)
        {
          this.people = new ObservableCollection<string>();
        }
        return this.people;
      }
    }

    private string legalDescription;
    public string LegalDescription
    {
      get { return this.legalDescription; }
      set { SetProperty(ref this.legalDescription, value); }
    }

    private string recorded;
    public string Recorded
    {
      get { return this.recorded; }
      set
      { 
        SetProperty(ref this.legalDescription, value);

        if (this.RecordedAsDate == null)
        {
          this.SetErrors(() => this.RecordedAsDate, String.Format("Cannot convert {0} to Date/Time", this.recorded));
        }
        else
        {
          this.ClearErrors(() => this.RecordedAsDate);
        }
      }
    }

    public DateTime? RecordedAsDate
    {
      get
      {
        DateTime dateTime;
        if (DateTime.TryParse(this.recorded, out dateTime))
        {
          return (DateTime?)dateTime;
        }
        return null;
      }
    }

    private int imagesCount;
    public int ImagesCount
    {
      get { return this.imagesCount; }
      set { SetProperty(ref this.imagesCount, value); }
    }

    private string imagesUrl;
    public string ImagesUrl
    {
      get { return this.imagesUrl; }
      set { SetProperty(ref this.imagesUrl, value); }
    }
  }
}
