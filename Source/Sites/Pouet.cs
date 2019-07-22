using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Conduit.Sites
{
  public class Pouet : ISite
  {
    private const string RecentlyReleased = "Recently released";
    private const string RecentlyAdded = "Recently added";

    public string ID => "pouet";
    public string Name => "Pouet.net";

    internal class Group
    {
      public int id { get; set; }
      public string name { get; set; }
    }
    internal class Prod
    {
      public int id { get; set; }
      public string name { get; set; }
      public string download { get; set; }
      public string releaseDate { get; set; }
      public List<Group> groups { get; set; }
      public SiteProdInfo ToProdInfo()
      {
        DateTime date;
        DateTime.TryParse(releaseDate, out date);
        return new SiteProdInfo()
        {
          Name = name,
          Group = groups?.FirstOrDefault()?.name,
          DownloadLink = download,
          ReleaseDate = date,
        };
      }
    }
    internal class ResponseProd
    {
      public bool success { get; set; }
      public Prod prod { get; set; }
    }
    internal class ResponseProdList
    {
      public bool success { get; set; }
      public Dictionary<string,Prod> prods { get; set; }
    }

    public IEnumerable<string> ProdLists => new List<string>()
    {
      RecentlyReleased,
      RecentlyAdded,
    };

    private async Task<T> GetAPIJSON<T>(string url) where T : class
    {
      using (var wc = new System.Net.WebClient())
      {
        string contents = await wc.DownloadStringTaskAsync(new Uri(url));
        if (contents != null && contents.Length > 0)
        {
          return JsonConvert.DeserializeObject<T>(contents);
        }
      }
      return null;
    }

    public async Task<IEnumerable<SiteProdInfo>> RetrieveProdList(string listName)
    {
      ResponseProdList response = null;
      if (listName == RecentlyReleased)
      {
        response = await GetAPIJSON<ResponseProdList>("http://api.pouet.net/v1/front-page/latest-released/");
      }
      else if (listName == RecentlyAdded)
      {
        response = await GetAPIJSON<ResponseProdList>("http://api.pouet.net/v1/front-page/latest-added/");
      }
      if (response == null || !response.success)
      {
        return null;
      }
      var result = new List<SiteProdInfo>();
      foreach (var prod in response.prods)
      {
        var prodInfo = prod.Value.ToProdInfo();
        prodInfo.ID = Convert.ToInt32(prod.Key);
        result.Add(prodInfo);
      }
      return result;
    }

    public async Task<SiteProdInfo> RetrieveProdInfo(Uri uri)
    {
      var response = await GetAPIJSON<ResponseProd>("http://api.pouet.net/v1/prod/?id=" + uri.Segments[2]);
      if (response == null || !response.success)
      {
        return null;
      }
      return response.prod.ToProdInfo();
    }
  }
}
