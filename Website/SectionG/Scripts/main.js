function submitLease() {
    console.log("submitLease!");
    ajaxPost("/Umbraco/Surface/Main/SubmitLease/", "", function (result) {
        console.log("submitLease: " + result);
    })
    return false;
}

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