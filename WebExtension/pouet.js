var span = document.getElementById("mainDownload");
if (span)
{
  var div = document.createElement("div");
  div.className = "pouettbl";
  div.id="pouetbox_conduit";

  div.innerHTML = "<h2>watch this demo with conduit</h2>\n"+
  "<div class='content'>"+
  CONDUIT.getLinkString("conduit://pouet/prod/"+parseInt(CONDUIT.getQueryVariable("which"),10)) +
  "</div>"+
  "</div>";

  var popHelper = document.getElementById("pouetbox_prodpopularityhelper");
  document.getElementById("prodpagecontainer").insertBefore(div,popHelper);
}
