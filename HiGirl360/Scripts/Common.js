
function AddFavorite(sURL, sTitle) {
    try { window.external.addFavorite(sURL, sTitle); }
    catch (e) {
        try { window.sidebar.addPanel(sTitle, sURL, ""); }
        catch (e) { alert("加入收藏失败，请使用Ctrl+D进行添加"); }
    }
}

function SetHome(obj, vrl) {
    try { obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(vrl); }
    catch (e) {
        if (window.netscape) {
            try { netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect"); }
            catch (e) {
                alert("温馨提示:\n浏览器不允许网页设置首页。\n请手动进入浏览器选项设置主页。");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', vrl);
        }
    }
} 

function SearchSelect(n, val) {
//    $id('ish').value = val;   
//    for (var i = 1; i < 4; i++) {
//        $id('ish_' + i).className = '';
//    }
//    $id('ish_' + n).className = 'on';
    $("#ish").val(val);
    $(".slt>a").removeClass();
    $("#ish_" + n).addClass("on");
} 