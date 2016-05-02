$(function() {
    $( ".draggable" ).draggable();
    $( ".draggable" ).resizable();
    // $( ".draggable" ).toggle("scale");
    $('.draggable').bind('wheel mousewheel', function(e){
        var delta;

        if (e.originalEvent.wheelDelta !== undefined)
            delta = e.originalEvent.wheelDelta;
        else
            delta = e.originalEvent.deltaY * -1;

        if(delta > 0) {
            $(this).css("width", "+=16");
            // $(this).css("height", "+=16");
        }
        else{
            $(this).css("width", "-=16");
            // $(this).css("height", "-=16");
        }

		// var scrollTo = null;
	
  //   	if (e.type == 'mousewheel') {
  //   	    scrollTo = (e.originalEvent.wheelDelta * -1);
  //   	}
  //   	else if (e.type == 'DOMMouseScroll') {
  //   	    scrollTo = 40 * e.originalEvent.detail;
  //   	}
	
    	// if (scrollTo) {
    	//     e.preventDefault();
    	//     $(this).scrollTop(scrollTo + $(this).scrollTop());
    	// }
        });
  });