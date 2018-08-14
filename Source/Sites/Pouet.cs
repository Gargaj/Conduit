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
    public string Host => "pouet";

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
    }
    internal class Response
    {
      public bool success { get; set; }
      public Prod prod { get; set; }
    }

    public async Task<SiteProdInfo> RetrieveProdInfo(Uri uri)
    {
      var url = new Uri("http://api.pouet.net/v1/prod/?id=" + uri.Segments[2]);

      using (var wc = new System.Net.WebClient())
      {
        string contents = await wc.DownloadStringTaskAsync(url);
        if (contents != null && contents.Length > 0)
        {
          var response = JsonConvert.DeserializeObject<Response>(contents);
          if (response.success)
          {
            DateTime date;
            DateTime.TryParse(response.prod.releaseDate, out date);
            SiteProdInfo info = new SiteProdInfo()
            {
              ID = response.prod.id,
              Name = response.prod.name,
              Group = response.prod.groups?.FirstOrDefault()?.name,
              DownloadLink = response.prod.download,
              ReleaseDate = date,
            };
            return info;
          }
        }
      }
      return null;
    }
  }
}
