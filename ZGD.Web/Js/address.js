//runxml
window.onload = loadXML;
//xmlDocument
var xmlDoc = null;

function initSelAddr(pName, cName, tName) {
    var p = document.getElementById(pName);
    var c = document.getElementById(cName);
    var ct = document.getElementById(tName);
    p.onchange = function () { getCity(p, c, ct) };
    c.onchange = function () { getCounty(p, c, ct) };
    initProvince(p);
}

//function initSelectInfo() {
//    var hp = document.getElementById('sprovince');
//    var hc = document.getElementById('scity');
//    var hct = document.getElementById('scounty');
//    hp.onchange = function () { getCity(hp, hc, hct) };
//    hc.onchange = function () { getCounty(hp, hc, hct) };
//    initProvince(hp);
//    
//}

//function initAddInfo() {
//    var hp = document.getElementById('hprovince');
//    var hc = document.getElementById('hcity');
//    var hct = document.getElementById('hcounty');
//    hp.onchange = function () { getCity(hp, hc, hct) };
//    hc.onchange = function () { getCounty(hp, hc, hct) };
//    initProvince(hp);

//    var lp = document.getElementById('lprovince');
//    var lc = document.getElementById('lcity');
//    var lct = document.getElementById('lcounty');
//    lp.onchange = function () { getCity(lp, lc, lct) };
//    lc.onchange = function () { getCounty(lp, lc, lct) };
//    initProvince(lp);
//}



function loadXML() {
    if (xmlDoc == null) {
        if (window.ActiveXObject) {//for ie
            xmlDoc = new ActiveXObject('Microsoft.XMLDOM');
        }
        else if (document.implementation && document.implementation.createDocument) {//for moz
            xmlDoc = document.implementation.createDocument('', '', null);
        }
        else {
            alert('xml read ERROR!');
            return null;
        }
        xmlDoc.async = false;
        xmlDoc.preserveWhiteSpace = false;
        xmlDoc.load('/files/area.xml');
    }
    return xmlDoc;
}



function initProvince(p) {
    loadXML();
	var provinces = xmlDoc.getElementsByTagName("province");
	
	//alert(provinces.length);
	
	
	while(p.options.length > 1){
		p.removeChild(p.options.item(1));	
	}

	for(var i = 0; i<provinces.length; i++){
		var oOption = document.createElement("option");
        //oOption.innerHTML = provinces[i].getAttribute('name');
		oOption.appendChild(document.createTextNode(provinces[i].getAttribute('name')));
        oOption.value = provinces[i].getAttribute('code');
        p.appendChild(oOption);	
	}
}



function getCity(p, c, ct) {
    try {
        while (c.options.length > 1) {
            c.removeChild(c.options.item(1));
        }

        while (ct.options.length > 1) {
            ct.removeChild(ct.options.item(1));
        }

        var name = p.options[p.selectedIndex].value;

        //ie work! but moz fail!
        var pro = ""; //  xmlDoc.selectSingleNode("//province[@name='"+name+"']");//xpath


        //ugly method but can work!
        var provinces = xmlDoc.getElementsByTagName("province");
        for (var k = 0; k < provinces.length; k++) {
            if (provinces[k].getAttribute('code') == name) {
                pro = provinces[k];
                break;
            }
        }

        if (pro != null) {
            var citys = pro.getElementsByTagName("city");
            if (citys != null) {
                for (var i = 0; i < citys.length; i++) {
                    var oOption = document.createElement("option");
                    //oOption.innerHTML = citys[i].getAttribute('name');
                    oOption.appendChild(document.createTextNode(citys[i].getAttribute('name')));
                    oOption.value = citys[i].getAttribute('code');
                    c.appendChild(oOption);
                }
            }
        }
    }
    catch (e) { }
}


function getCounty(p, c, ct) {
    try {
        while (ct.options.length > 1) {
            ct.removeChild(ct.options.item(1));
        }
        var name = c.options[c.selectedIndex].value;

        //ie work! but moz fail!
        //var pro = xmlDoc.selectSingleNode("//province[@name='"+name+"']");//xpath


        //ugly method but can work!
        var pro = null;
        var citys = xmlDoc.getElementsByTagName("city");
        for (var k = 0; k < citys.length; k++) {
            if (citys[k].getAttribute('code') == name) {
                pro = citys[k];
                break;
            }
        }

        if (pro != null) {
            var countys = pro.getElementsByTagName("county");
            if (countys != null) {
                for (var i = 0; i < countys.length; i++) {
                    var oOption = document.createElement("option");
                    //oOption.innerHTML = citys[i].getAttribute('name');
                    oOption.appendChild(document.createTextNode(countys[i].getAttribute('name')));
                    oOption.value = countys[i].getAttribute('code');
                    ct.appendChild(oOption);
                }
            }
        }
    }
    catch (e) { }

}
