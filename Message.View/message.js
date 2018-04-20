$( document ).ready(function() {
    getData();
    window.setInterval(function(){
        getData();
      }, 1000);
      $('#message').keydown(function(e){
        if (e.keyCode == 13 && !e.shiftKey) {
            postData();
        }
    });
});

function getData() {
    var ul = $('<ul class="list-unstyled">');
    $.getJSON('/m', function(data) {
        $.each(data, function (index, inData) {
            ul.append($(document.createElement('li')).text(inData)
            );
        });
        $('#messageArea').empty().append(ul);   
    });
}

function postData() {
    $.post( "/m", { message: $("#username").val() + ": " + $("#message").val() }, function( data ) {
        $("#message").val('');
   });
}