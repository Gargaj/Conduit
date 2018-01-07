using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Sites
{
  public class SiteProdInfo
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Group { get; set; }
    public string DownloadLink { get; set; }
    public DateTime ReleaseDate { get; set; }
  }
  public interface ISite
  {
    string Host { get; }
    Task<SiteProdInfo> RetrieveProdInfo(Uri uri);
  }
}
