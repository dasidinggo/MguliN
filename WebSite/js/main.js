function tabON(ind)
{
	$(".tmn").css("background","url(image/tabMenuoff.jpg) no-repeat top").css("color","#000");
	$("#tmen"+ind).css("background","url(image/tabMenuon.jpg) no-repeat top").css("color","#fff");
	$(".tabBox1").css("display","none");
	$("#tab"+ind).css("display","");
}

// $(document).ready(function(){
// 	$("#zxkfbtn").click(function(){
// 		if(kfClick) return;
// 		kfClick = true;
// 		var ileft = 0;
//   		if($(".fbox").css("right") == "0px")
// 			ileft = -151;
// 		else
// 			ileft = 0;
// 		$(".fbox").animate({right:ileft},"slow",function (){kfClick = false;});
// 	});
// 	ter = setInterval("autocban()",3000);
// });



function swtrcon(id,til)
{
	$(".lmen").css("color","#666");
	$("#lmen" + id.split('_')[1]).css("color","#114b8b");
	$("#divtitle").text(til);
	$(".cdetail_right").css("display","none");
	$("#"+id).css("display","");
}

function getQueryString(name) 
{ 
	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i"); 
	var r = window.location.search.substr(1).match(reg); 
	if (r != null) return unescape(r[2]); return null; 
} 

