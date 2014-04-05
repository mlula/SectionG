function ajaxPost(url, data, callBack) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        cache: false,
        dataType: "text",
        success: callBack,
        error: function (result) {
            console.log("Ajax error: " + result);
        }
    });
}