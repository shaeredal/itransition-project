$(function() {
    $( ".frame-image" ).draggable();
    $( ".frame-image" ).resizable();
    // $( ".draggable" ).toggle("scale");
    $('.frame-image').bind('mousewheel', function(e){
        // var delta;

        // if (e.originalEvent.wheelDelta !== undefined)
        //     delta = e.originalEvent.wheelDelta;
        // else
        //     delta = e.originalEvent.deltaY * -1;

        if(e.originalEvent.deltaY < 0) {
            $(this).css("width", "+=16");
            $(this).css("height", "+=16");
        }
        else{
            $(this).css("width", "-=16");
            $(this).css("height", "-=16");
        }
        return false;
    });
});