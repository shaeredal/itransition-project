function DelComment(id) {

        jQuery.ajax({
            type: "POST",
            url: "/User/DelComment",
            data: JSON.stringify({ data: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            succses: window.location.href = "/User/UserInfo/" + getUserName()

        });
}

function DelComix(id) {

    jQuery.ajax({
        type: "POST",
        url: "/User/DelComix",
        data: JSON.stringify({ data: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        succses: window.location.href = "/User/UserInfo/" + getUserName()

    });
}

function getUserName() {
    var id = document.getElementById("UserName").textContent;
    return id;
}