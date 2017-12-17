
$(document).ready(function(){
	$("dd:not(:eq(0))").hide();
	$("dt a").click(function(){
		$("dd:visible").slideUp("slow");
		$(this).parent().next().slideDown("slow");
		return false;
	});
});
