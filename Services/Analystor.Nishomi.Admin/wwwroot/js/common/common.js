var common = {
    genericAjaxCall: function (url, data, type, successCallback, errorCallBack, dataType = 'json', contentType = 'application/json') {
        $.ajax({
            type: type,
            url: url,
            data: JSON.stringify(data),
            contentType: contentType,
            dataType: dataType,
            success: successCallback,
            error: errorCallBack
        });
    },

    convertDateToEnIN: function (date, currentFormat) {
        //need to implement logic for supporting differnt format conversion now it wil convert only mm-dd-yyyy to dd-mm-yyyy
        var reg = /(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d/;
        if (date.match(reg)) {
            var datearray = date.split("-");
            var newdate = datearray[1] + '-' + datearray[0] + '-' + datearray[2];
            return newdate;
        }
        else return null;
    },

    toBoolean: function (boolText) {
        if (boolText.to == "False") {
            return false;
        }
        else if (boolText == "True") {
            return true;
        }
    },

    processDate: function (date) {
        var parts = date.split("-");
        return new Date(parts[2], parts[1] - 1, parts[0]);
    },

    toDate : function (dateString) {
        var from = dateString.split("-")
        return new Date(from[2], from[1] - 1, from[0])
    },

    isUUID : function  ( uuid ) {
    let s = "" + uuid;

    s = s.match('^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$');
    if (s === null) {
      return false;
    }
    return true;
    },

    convertToNumber: function (numberString) {
        return isNaN(parseFloat(numberString)) ? 0 : parseFloat(numberString)
    },

    getMonths: function () {
        const monthNames = ["January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        ];
        return monthNames;
    }, 

    swalCommonErrorMessages: function (text) {
        Swal.fire({
            title: "",
            text: text,
            type: 'info',
            button: "Ok",
        });
    },

    getColor: function (i) {
        switch (i) {
            case 0:
                return "#ffb200";
            case 1:
                return "#ff2e00";
            case 2:
                return "#f57542";
            case 3:
                return "#42bff5";
            case 4:
                return "#0094ff";
            case 5:
                return "#00f645";
            case 6:
                return "#b9d705";
            case 7:
                return "#07c569";
            default:
                return "#0fd109";
        }
    },

    getClassForProspectStatusReport: function (i) {
        switch (i) {
            case 0:
                return "ps1";
            case 1:
                return "ps2";
            case 2:
                return "ps3";
            case 3:
                return "ps4";
            case 4:
                return "ps5";
            case 5:
                return "ps6";
            default:
                return "ps1";
        }
    }
};

onAjaxSuccess = (data) => {
    debugger;
    var result = data;
    let title = "";
    let text = "";
    let type = "";
    if (result) {
        if (result.status) {
            title = "Success";
            text = result.message;
            type = 'success';
        }
        else {
            title = "Failure";
            text = result.message;
            type = 'error';
        }

        Swal.fire({
            title: title,
            text: text,
            type: type,
            button: "Ok",
        }).then((result) => {
            if (result.value) {
                var currentPath = window.location.pathname;
                var splitPath = currentPath.split("/");
                var controller = splitPath[1];
                var action = "index";
                window.location.href = '/' + controller+'/'+action+'';
            }
        });
    }
};

onAjaxFailure = (data) => {
    Swal.fire({
        title: "Failure",
        text: "Some error occured, Please contact administrator!",
        type: 'error',
        button: "Ok",
    });
};

onAjaxSuccessWithRedirect = (data) => {
    debugger;
    var result = data;
    let title = "";
    let text = "";
    let type = "";
    if (result) {
        if (result) {
                title = "Success";
                text = result.message;
                type = 'success';
        }
        else {
            title = "Failure";
            text = result.message;
            type = 'error';
        }

        Swal.fire({
            title: title,
            text: text,
            type: type,
        }).then((result) => {
            if (result.value) {
                location.reload();
            }
        });
    }
};

onAjaxSuccessWithMessage = (data) => {
    debugger;
    var result = data;
    let title = "";
    let text = "";
    let type = "";
    if (result) {
        if (result.status) {
            title = "Success";
            text = result.message;
            type = 'success';
        }
        else {
            title = "Failure";
            text = result.message;
            type = 'error';
        }

        Swal.fire({
            title: title,
            text: text,
            type: type,
        }).then((result) => {
            if (result.value) {
                location.reload();
            }
        });
    }
};


onAjaxSuccessNoRedirect = (data) => {
    debugger;
    var result = data;
    let title = "";
    let text = "";
    let type = "";
    if (result) {
        if (result) {
            if (result.status) {
                title = "Success";
                text = result.message;
                type = 'success';
            }
            else {
                title = "Failure";
                text = result.message;
                type = 'error';
            }
        }
        else {
            title = "Failure";
            text = result.message;
            type = 'error';
        }

        Swal.fire({
            title: title,
            text: text,
            type: type,
        }).then((result) => {
            if (result.value) {
                debugger;
                var currentPath = window.location.pathname;
                var fullpath = window.location.search;
                var splitPath = currentPath.split("/");
                var controller = splitPath[1];
                var split = fullpath.split("=");
                var url = splitPath[2]+split[0]+"="+ split[1];
                window.location.href = '/' + controller + '/' + url + '=false';
            }
        });
    }
};

onAjaxBefore = (data) => {
    $("#spinner").removeClass('d-none')
    $("#savePlan").hide();
};

onAjaxAfter = (data) => {
    $("#spinner").addClass('d-none')
    $("#savePlan").show();
};

$(document).on('click', '.grd-parent-checkbox', function () {
    if ($(this).is(':checked')) {
        $('.grd-child-checkbox').prop('checked', true);
    }
    else {
        $('.grd-child-checkbox').prop('checked', false);
    }
});

$(document).on('keyup', '.fixed-two-decimal', function () {
    var amt = $(this);
    if (isNaN(amt.val())) {
        amt.val(0);
    }
    if (amt.val().indexOf(".") > -1 && (amt.val().split('.')[1].length > 2)) {
        amt.val(amt.val().substring(0, amt.val().length - 1));
    }
});

$(document).on('keydown', '.positive-number', function (e) {
    var key = e.charCode || e.keyCode || 0;
    // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
    // home, end, period, and numpad decimal
    return (
        key == 8 ||
        key == 9 ||
        key == 13 ||
        key == 46 ||
        key == 110 ||
        key == 190 ||
        (key >= 35 && key <= 40) ||
        (key >= 48 && key <= 57) ||
        (key >= 96 && key <= 105));
});

userCreateAjaxSuccess = (data) => {
var result = data;
if (result) {
    if (result.status) {
        Swal.fire({
            title: "Success",
            text: result.message,
            type: "success",
            button: "Ok",
        }).then((result) => {
            if (result.value) {
                var currentPath = window.location.pathname;
                var splitPath = currentPath.split("/");
                var controller = splitPath[1];
                var action = "index";
                window.location.href = '/' + controller + '/' + action + '';
            }
        });
    }
    else {
        Swal.fire({
            title: "Failure",
            text: result.message,
            type: "error",
            button: "Ok",
        }).then((result) => {
            if (result.value) {
            }
        });
    }
}
};