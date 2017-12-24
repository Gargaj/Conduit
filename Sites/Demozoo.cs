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
    public string Host => "demozoo";

    internal class AuthorNicks
    {
      public string name { get; set; }
    }

    internal class DownloadLink
    {
      public string url { get; set; }
    }

    internal class Response
    {
      public int id { get; set; }
      public string title { get; set; }
      public List<AuthorNicks> author_nicks { get; set; }
      public List<DownloadLink> download_links { get; set; }
      public DateTime release_date { get; set; }
    }
    public async Task<SiteProdInfo> RetrieveProdInfo(Uri uri)
    {
      var url = new Uri("http://demozoo.org/api/v1/productions/" + uri.Segments[2] + "/?format=json");

      using (var wc = new System.Net.WebClient())
      {
        string contents = await wc.DownloadStringTaskAsync(url);
        if (contents != null && contents.Length > 0)
        {
          var response = JsonConvert.DeserializeObject<Response>(contents);
          if (response != null && response.title != null)
          {
            SiteProdInfo info = new SiteProdInfo()
            {
              ID = response.id,
              Name = response.title,
              Group = response.author_nicks?.FirstOrDefault()?.name,
              DownloadLink = response.download_links?.FirstOrDefault()?.url,
              ReleaseDate = response.release_date,
            };
            return info;
          }
        }
      }
      return null;
    }
  }
}
