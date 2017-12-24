var re = /productions\/(\d+)/ig;
var prodID = re.exec(location.href);

var right = document.getElementsByClassName("right")[0];

var block = document.createElement("div");
block.innerHTML = 
  "<h3>Download and run</h3>\n"+
  "<div style='clear:both;margin:10px 0px'><a href='conduit://demozoo/prod/"+prodID[1]+"' id='fakeDownloadLink'>Click here to download and run prod with Conduit</a></div>";
block.className = "panel";

right.insertBefore(block,right.childNodes[0]);