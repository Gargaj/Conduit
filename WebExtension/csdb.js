var download_table = document.querySelector("#downloadLinks");
if (download_table)
{
  var after = download_table.nextSibling;
  
  download_table.parentNode.insertBefore(document.createElement("br"),after);
  
  var b = document.createElement("b");
  b.innerHTML = "Download and run with Conduit :";
  download_table.parentNode.insertBefore(b,after);
  download_table.parentNode.insertBefore(document.createElement("br"),after);
  
  var div = document.createElement("div");
  download_table.parentNode.insertBefore(div,after);
  div.setAttribute("id","conduit");
  
  var id = CONDUIT.getQueryVariable("id");
  div.innerHTML = CONDUIT.getLinkString("conduit://csdb/release/"+parseInt(id,10));
}