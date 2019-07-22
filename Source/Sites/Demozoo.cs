using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Conduit.Sites
{
  class Demozoo : ISite
  {
    public string ID => "demozoo";
    public string Name => "Demozoo";

    internal class AuthorNicks
    {
      public string name { get; set; }
    }

    internal class DownloadLink
    {
      public string url { get; set; }
    }

    internal class ResponseProd
    {
      public int id { get; set; }
      public string title { get; set; }
      public List<AuthorNicks> author_nicks { get; set; }
      public List<DownloadLink> download_links { get; set; }
      public string release_date { get; set; }
      public SiteProdInfo ToProdInfo()
      {
        DateTime date;
        DateTime.TryParse(release_date, out date);
        return new SiteProdInfo()
        {
          ID = id,
          Name = title,
          Group = author_nicks?.FirstOrDefault()?.name,
          DownloadLink = download_links?.FirstOrDefault()?.url,
          ReleaseDate = date,
        };
      }
    }

    internal class ResponseProdList
    {
      public List<ResponseProd> results { get; set; }
    }

    public IEnumerable<string> ProdLists => new List<string>()
    {
      "Recently added",
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
      var response = await GetAPIJSON<ResponseProdList>("http://demozoo.org/api/v1/productions/?format=json");
      if (response == null || response.results == null)
      {
        return null;
      }
      var result = new List<SiteProdInfo>();
      foreach (var prod in response.results)
      {
        result.Add(prod.ToProdInfo());
      }
      return result;
    }

    public async Task<SiteProdInfo> RetrieveProdInfo(Uri uri)
    {
      var response = await GetAPIJSON<ResponseProd>("http://demozoo.org/api/v1/productions/" + uri.Segments[2] + "/?format=json");
      if (response == null || response.title == null)
      {
        return null;
      }
      return response.ToProdInfo();
    }
  }
}
