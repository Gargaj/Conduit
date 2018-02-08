function getQueryVariable(variable) {
  var query = window.location.search.substring(1);
  var vars = query.split('&');
  for (var i = 0; i < vars.length; i++) {
    var pair = vars[i].split('=');
    if (decodeURIComponent(pair[0]) == variable) {
      return decodeURIComponent(pair[1]);
    }
  }
  console.log('Query variable %s not found', variable);
}

var span = document.getElementById("mainDownload");
if (span)
{
  var div = document.createElement("div");
  div.className = "pouettbl";
  div.id="pouetbox_conduit";

  div.innerHTML = "<h2>watch this demo with conduit</h2>\n"+
  "<div class='content'>"+
  "<a href='conduit://pouet/prod/"+parseInt(getQueryVariable("which"),10)+"'><b>Click here</b></a> to download and watch this prod immediately with <a href='"+CONDUIT_URL+"'>Conduit</a>"+
  "</div>"+
  "</div>";

  var popHelper = document.getElementById("pouetbox_prodpopularityhelper");
  document.getElementById("prodpagecontainer").insertBefore(div,popHelper);
}
