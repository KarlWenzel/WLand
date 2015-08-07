using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WLand.Common.Lib;

namespace WLand.Modules.HtmlParser
{
  public interface IOkCountyRecParser { }

  [Export(typeof(IOkCountyRecParser))]
  public class OkCountyRecParser : IOkCountyRecParser
  {
    public const string TABLE_ID = "results-table";

    public static IEnumerable<OkCountyRecord> FromHtml(string html)
    {
      var records = new List<OkCountyRecord>();
      try
      {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        foreach (var tr in doc.GetElementbyId(OkCountyRecParser.TABLE_ID).SelectNodes("tbody/tr"))
        {
          var tdNodes = tr.SelectNodes("td");

          int imagesCount = 0;
          string imagesUrl = "";
          string imageInnerText = tdNodes[9].InnerText;

          string pattern = @"(?<imageCount>\d+)\s+[:] <a[^>]+href=\""(?<imageUrl>.*?)\""[^>]*>.*?</a>";
          var imageRegexMatches = new Regex(pattern).Match(imageInnerText);
          
          if (imageRegexMatches.Success)
          {
            imagesCount = int.Parse(imageRegexMatches.Groups["imageCount"].Value);
            imagesUrl = imageRegexMatches.Groups["imageUrl"].Value;
          }

          var rec = new OkCountyRecord()
          {
            County = tdNodes[1].InnerText,
            Instrument = tdNodes[2].InnerText,
            RecordType = tdNodes[3].InnerText,
            Book = tdNodes[4].InnerText,
            Pages = tdNodes[5].InnerText,
            LegalDescription = tdNodes[7].InnerText,
            Recorded = tdNodes[8].InnerText,
            ImagesCount = imagesCount,
            ImagesUrl = imagesUrl
          };

          string people = tdNodes[6].InnerText;
          foreach (string person in people.Split(new string[] { "<br>", "<br/>", "<br />"}, 100, StringSplitOptions.RemoveEmptyEntries ))
          {
            rec.People.Add(person);
          }

          records.Add(rec);
        }
      }
      catch (Exception) { }

      return records;
    }
  }
}
