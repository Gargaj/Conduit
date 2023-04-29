var CONDUIT = {
  URL: "https://github.com/Gargaj/Conduit",
  getQueryVariable : function(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split('&');
    for (var i = 0; i < vars.length; i++) {
      var pair = vars[i].split('=');
      if (decodeURIComponent(pair[0]) == variable) {
        return decodeURIComponent(pair[1]);
      }
    }
    console.log(`Query variable {variable} not found`);
  },
  getLinkString : function(url) {
    return "<a href='"+url+"'><b>Click here</b></a> to download and watch this prod immediately with <a href='"+CONDUIT.URL+"'>Conduit</a>"
  }
}