var span = document.getElementById("mainDownload");
var link = document.getElementById("mainDownloadLink");

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

if (link)
{
  var snort = [
    "*oink*",  // en
    "*röff*",  // hu
    "*groin*", // fr
    "*grunz*", // de
    "*röh*",   // fi
    "*&oslash;f*", // dk
  ]
  span.innerHTML = "[<a href='conduit://pouet/prod/"+parseInt(getQueryVariable("which"),10)+"' id='fakeDownloadLink' style='color:red;'>"+snort[ Math.floor(Math.random()*snort.length) ]+"</a>] " + span.innerHTML;
}
