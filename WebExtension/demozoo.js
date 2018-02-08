var download_links = document.getElementsByClassName("download_links");
if(download_links && download_links.length)
{
  var re = /productions\/(\d+)/ig;
  var prodID = re.exec(location.href);

  var right = document.getElementsByClassName("right")[0];

  var block = document.createElement("div");
  block.id = "conduit";
  block.innerHTML = 
    "<h3>Download and run</h3>\n"+
    "<div><a href='conduit://demozoo/prod/"+parseInt(prodID[1],10)+"'><b>Click here</b></a> to download and watch this prod immediately with <a href='"+CONDUIT_URL+"'>Conduit</a>"
  block.className = "panel";

  right.insertBefore(block,right.childNodes[0]);
}
