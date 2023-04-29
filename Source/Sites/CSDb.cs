using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Conduit.Sites
{
  class CSDb : ISite
  {
    public string ID => "csdb";
    public string Name => "CSDb";

    public IEnumerable<string> ProdLists => new List<string>()
    {
    };

    public async Task<IEnumerable<SiteProdInfo>> RetrieveProdList(string listName)
    {
      return new List<SiteProdInfo>();
    }

    public async Task<SiteProdInfo> RetrieveProdInfo(Uri uri)
    {
      var id = uri.Segments[2];
      using (var wc = new System.Net.WebClient())
      {
        var csdbApiURL = $"https://csdb.dk/webservice/?type=release&id={id}";
        string contents = await wc.DownloadStringTaskAsync(csdbApiURL);
        if (contents == null || contents.Length <= 0)
        {
          return null;
        }

        XmlDocument document = new XmlDocument();
        try
        {
          document.LoadXml(contents);
        }
        catch (XmlException)
        {
          return null;
        }

        SiteProdInfo prodInfo = new SiteProdInfo();
        prodInfo.ID = Convert.ToInt32(id);
        prodInfo.Name = document?.DocumentElement?.SelectSingleNode("//Release/Name")?.InnerText ?? string.Empty;

        var groups = document?.DocumentElement?.SelectNodes("//Release/ReleasedBy/Group");
        if (groups != null)
        {
          var groupNames = new List<string>();
          foreach (XmlNode group in groups)
          {
            groupNames.Add(group.SelectSingleNode("Name").InnerText);
          }
          prodInfo.Group = string.Join(" & ", groupNames);
        }

        prodInfo.DownloadLink = document?.DocumentElement?.SelectSingleNode("//Release/DownloadLinks/DownloadLink/Link")?.InnerText ?? string.Empty;

        prodInfo.ReleaseDate = new DateTime(
          Convert.ToInt32(document?.DocumentElement?.SelectSingleNode("//Release/ReleaseYear")?.InnerText ?? "0"),
          Convert.ToInt32(document?.DocumentElement?.SelectSingleNode("//Release/ReleaseMonth")?.InnerText ?? "0"),
          Convert.ToInt32(document?.DocumentElement?.SelectSingleNode("//Release/ReleaseDay")?.InnerText ?? "0")
        );
        return prodInfo;
      }
    }
  }
}
