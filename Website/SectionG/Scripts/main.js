function submitLease() {
    console.log("submitLease!");
    var request = new Lease();
    var oTable;

    ajaxPost("/Umbraco/Surface/Main/SubmitLease/", request, function (result) {
        console.log("submitLease: " + JSON.parse(result).aaData);
        $('#step4').hide();
        $('#step5').show();
        var obj = JSON.parse(result);
        var aaData = [];
        $.each(obj, function () {
            var aData = [];

            // Mettre le data dans la table
            if (this['AppartmentNumber'] != null && this['AppartmentNumber'] != '')
                aData.push(this["AddressNumber"] + " " + this["Street"] + " #" + this["AppartmentNumber"]);
            else
                aData.push(this["AddressNumber"] + " " + this["Street"]);

            aData.push(this["Price"] + " $");

            var myDate = new Date(parseInt(this["DateCreated"].substr(6)));
            aData.push(myDate.getFullYear() + "-" + (myDate.getMonth() < 10 ? '0' : '') + (myDate.getMonth()+1) + "-" + (myDate.getDate() < 10 ? '0' : '') + myDate.getDate());

            aaData.push(aData);
        });

        console.log(aaData);

        // Datatable
        oTable = $('#registre').dataTable({
            "sDom": "<'row'<'col-6'f><'col-6'l>r>t<'row'<'col-6'i><'col-6'p>>",
            "sPaginationType": "bootstrap",
            "aLengthMenu": [5, 10, 25, 50],
            "bFilter": false,
            "oLanguage": {
                "sLengthMenu": "_MENU_ items par page",
                "sInfo": "Montrer de _START_ à _END_ de _TOTAL_ entrées",
                "sSearch": "Rechercher",
                "oPaginate": {
                    "sPrevious": "Précédent",
                    "sNext": "Suivant"
                }
            },
            "aaData": aaData
        });        
    })
    
    return false;
}

function goToRegistry() {
    // ...
    console.log("goToRegistry!");
    var request = new Lease();
    var oTable;

    ajaxPost("/Umbraco/Surface/Main/GetLeases/", request, function (result) {
        console.log("submitLease: " + JSON.parse(result).aaData);
        $('#step1').hide();
        $('#step2').show();
        var obj = JSON.parse(result);
        var aaData = [];
        $.each(obj, function () {

            var aData = [];

            // Mettre le data dans la table
            if (this['AppartmentNumber'] != null && this['AppartmentNumber'] != '') {
                aData.push(this["AddressNumber"] + " " + this["Street"] + " #" + this["AppartmentNumber"]);
            }
            else
                aData.push(this["AddressNumber"] + " " + this["Street"]);
            
            aData.push(getDate(this["StartDate"]));
            aData.push(getDate(this["EndDate"]));
            aData.push(this["Price"] + " $");

            aaData.push(aData);
        });

        console.log(aaData);

        // Datatable
        oTable = $('#registre').dataTable({
            "sDom": "<'row'<'col-6'f><'col-6'l>r>t<'row'<'col-6'i><'col-6'p>>",
            "sPaginationType": "bootstrap",
            "aLengthMenu": [5, 10, 25, 50],
            "oLanguage": {
                "sLengthMenu": "_MENU_ items par page",
                "sInfo": "Montrer de _START_ à _END_ de _TOTAL_ entrées",
                "sSearch": "Rechercher",
                "oPaginate": {
                    "sPrevious": "Précédent",
                    "sNext": "Suivant"
                }
            },
            "aaData": aaData
        });
    })
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

function getDate(jsonDate) {
    var date = new Date(parseInt(jsonDate.substr(6)));
    return date.getUTCFullYear() + "-" + (date.getUTCMonth() < 10 ? '0' : '') + (date.getUTCMonth() + 1) + "-" + (date.getUTCDate() < 10 ? '0' : '') + date.getUTCDate();
}

function Lease() {
    var self = this;
    self.Price = $("#Prix").val();
    self.AddressNumber = $("#Adresse").val();    
    self.AppartmentNumber = $("#Appt").val();
    self.Street = $("#Rue").val();
    self.PostalCode = $("#CodePostal").val();
    self.City = $("#Ville").val();
    self.BoroughId = $("#Arrondissement").val();
    self.StartDate = $("#dateDebutAlt").val();
    console.log("Start Date = " + self.StartDate);
    self.EndDate = $("#dateFinAlt").val();
    console.log("End Date = " + self.EndDate);
    self.Detail = $("details").val();
    // self.Inclusions
}
