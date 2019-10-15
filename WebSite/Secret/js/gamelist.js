function Svgameinfo() {
    getsvStr();
}

var curpage = 1;
var pageCount;
var pagesize = 6;
$(function () {
    BindData();
});

function editGame(id) {
    window.parent.mainFrame.location.href = 'dd.html?id=' + id;
}

var gamejson;
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

function paging(e) {
    $(".pagination li").removeClass("active");
    $("#pg" + e).addClass("active");
    curpage = e;
    resetlipp();
    pagingdate();
}

function preornextpage(pp) {
    
    if (pp == -1 && curpage == 1)
        return;
        
    if (pp == 1 && curpage == pageCount)
        return;
    resetlipp();
    paging(curpage + pp);
}

function resetlipp() {
    $(".lipp").removeClass("disabled");
    if (curpage == 1) {
        $("#lipre").addClass("disabled");
    }

    if (curpage == pageCount) {
        $("#linext").addClass("disabled");
    }
}

function pagingdate() {
    //加载数据
    $.ajax({
        type: "post",
        url: "../Handler/Handler.ashx?ct=GetGameList&pagesize=" + pagesize + "&pages=" + curpage + "&mWhere=" + mWhereStr,
        success: function (result) {
            if (result.length > 0) {
                pageCount = Math.ceil(result.split('&')[1] / pagesize);
                if (pageCount == 0)
                    return;
                gamejson = $.parseJSON(result.split('&')[0]);

                //清空数据
                $("#contentlist").html("");
                //绑定数据
                for (var i = 0; i < gamejson.length; i++) {
                    $("#contentlist").append('<li class="list-group-item"><table><tr><td class="tconcole"><img src="images/'
                        + gamejson[i].console + '.jpg" /></td><td><h4 class="list-group-item-heading">'
                        + gamejson[i].name + '</h4><p class="list-group-item-text">购买日期：' + gamejson[i].buydate
                        + '   金额：<font>' + gamejson[i].price + '</font></p></td></tr ></table >'
                        + '<button type="button" onclick="editGame(' + gamejson[i].id
                        + ')" class="btn btn-primary btn-sm pull-right">编辑</button></li>');
                }

            }
        },
        error: function (err) {
            alert("查询错误");
        }
    });
}

function BindData() {
    $(".pagination").html("");
    $("#contentlist").html("");
    $.ajax({
        type: "post",
        url: "../Handler/Handler.ashx?ct=GetGameList&pagesize=" + pagesize + "&mWhere=" + mWhereStr,
        success: function (result) {
            if (result.length > 0) {
                pageCount = Math.ceil(result.split('&')[1] / pagesize);
                if (pageCount == 0)
                    return;
                gamejson = $.parseJSON(result.split('&')[0]);
                
                //初始化分页控件
                $(".pagination").append('<li id="lipre" class="lipp disabled" onclick="preornextpage(-1)"><a href="#1">&laquo;</a></li>');
                for (var j = 1; j <= pageCount; j++) {
                    $(".pagination").append('<li id="pg' + j + '" onclick="paging(' + j + ')"><a href="#1">' + j + '</a></li>');
                }
                $(".pagination").append('<li id="linext" class="lipp" onclick="preornextpage(1)"><a href="#1">&raquo;</a></li>');
                $(".pagination").children().eq(1).addClass("active");
                //$(".pagination").children(1).addClass("active");

                //绑定数据
                for (var i = 0; i < gamejson.length; i++) {
                    $("#contentlist").append('<li class="list-group-item"><table><tr><td class="tconcole"><img src="images/'
                        + gamejson[i].console + '.jpg" /></td><td><h4 class="list-group-item-heading">'
                        + gamejson[i].name + '</h4><p class="list-group-item-text">购买日期：' + gamejson[i].buydate
                        + '   金额：<font>' + gamejson[i].price + '</font></p></td></tr ></table >'
                        + '<button type="button" onclick="editGame(' + gamejson[i].id
                        + ')" class="btn btn-primary btn-sm pull-right">编辑</button></li>');
                }

            }
        },
        error: function (err) {
            alert("查询错误");
        }
    });
}
var mWhereStr = "";
function query() {
    mWhereStr = "";
    var isSering = 0;
    $(".Qninput").each(function () {
        if ($(this).val() != "")
        {
            mWhereStr += $(this).attr("id").split('_')[1] + ":" + $(this).val() + ";";
            isSering = 1;
        }
            
    });
    mWhereStr = mWhereStr.substr(0, mWhereStr.length - 1);
    BindData();
    if (isSering == 1)
        $("#btnSearch").addClass("btnser");
    else
        $("#btnSearch").removeClass("btnser");
}

function Serclear() {
    $("#drn_console").val("");
    $("#txt_name").val("");
    $("#drn_year").val("");
    //$("#btnSearch").removeClass("btnser");
}