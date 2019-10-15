function Svgameinfo() {
    $("#btnSave").attr("disabled", "true");
    getsvStr();
    //alert(svstr); return;
    var rulr = "../Handler/Handler.ashx?ct=AddGameInfo&svstr=" + svstr ;
    if (id != "" && id != undefined) {
        rulr = "../Handler/Handler.ashx?ct=UpdGameInfo&svstr=" + svstr + "&id=" + id;
    }
    $.ajax({
        type: "post",
        url: rulr,
        success: function (result) {
            if (result != "0")
            {
                setalert("保存成功！");
                //window.parent.mainFrame.location.href = 'list.html'
            }
            else
                setalert("保存失败！");
            
        },
        error: function (err) {
            setalert("保存失败！");
        }
    });
}
var id;
$(function () {
    if ($.request.queryString["id"] != "" && $.request.queryString["id"] != undefined)
        id = $.request.queryString["id"]
    else id = "";
    if (id != "") {
        $.ajax({
            type: "post",
            url: "../Handler/Handler.ashx?ct=GetGameInfo&id=" + id,
            success: function (result) {
                //alert(result);
                gamejson = $.parseJSON(result);
                $("#drn_console").val(gamejson[0].console);
                $("#txt_name").val(gamejson[0].name);
                $("#txt_price").val(gamejson[0].price);
                $("#txt_buydate").val(gamejson[0].buydate);
            },
            error: function (err) {
                setalert("加载失败！");
            }
        });
    }
    

    var mbdate = new Date();
    if ($("#txt_buydate").val() == "")
        $("#txt_buydate").val(mbdate.getFullYear() + "-" + (mbdate.getMonth()+1) + "-" + mbdate.getDate())
});
var svstr = "";
function getsvStr() {
    svstr = "";
    $("input[id*=txt_]").each(function () {
        svstr += $(this).attr("id").split('_')[1] + "|" + $(this).val() + "@";
    });
    $("select[id*=drn_]").each(function () {
        svstr += $(this).attr("id").split('_')[1] + "|" + $(this).val() + "@";
    });
    svstr = svstr.substr(0, svstr.length - 1);
}


function setalert(str) {
    $("#alertxt").text(str);
    if (str == "保存失败！") {
        $("#tbalert").show();
        $("#layerTB").hide();
    } else {
        $("#tbalert").hide();
        $("#layerTB").show();
        
    }
    $("#btnSave").removeAttr("disabled");
}