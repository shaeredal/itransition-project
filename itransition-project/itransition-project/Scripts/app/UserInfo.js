function DelComment(id) {

        jQuery.ajax({
            type: "POST",
            url: "/User/DelComment",
            data: JSON.stringify({ data: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    }