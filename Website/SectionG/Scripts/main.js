function submitLease() {
    console.log("submitLease!");
    ajaxPost("/Umbraco/Surface/Main/SubmitLease/", "", function (result) {
        console.log("submitLease: " + result);
        $('#step3').hide();
        $('#step4').show();
    })
    return false;
}

function goToRegistry() {
    // ...
    console.log("goToRegistry!");
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