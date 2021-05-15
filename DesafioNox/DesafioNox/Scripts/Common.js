var Common = {
    Ajax: function (httpMethod, controller, method, data, successCallBack, async, cache) {

        let loading = $('body').find('div').find(".lds-ellipsis");
        loading.css('display', 'block');

        if (typeof async == "undefined") {
            async = true;
        }
        if (typeof cache == "undefined") {
            cache = false;
        }

        var ajaxObj = $.ajax({
            type: httpMethod.toUpperCase(),
            url: '/' + controller + '/' + method,
            data: data,
            dataType: "JSON",
            async: async,
            cache: cache,
            success: successCallBack,
            error: function (err, type, httpStatus) {
                Common.AjaxFailureCallback(err, type, httpStatus);
            }
        });

        setTimeout(function () {
            loading.css('display', 'none');
        }, 200);

        return ajaxObj;
    },

    AjaxFailureCallback: function (err, type, httpStatus) {
        var failureMessage = 'Error occurred in ajax call' + err.status + " - " + err.responseText + " - " + httpStatus;
        console.log(failureMessage);
        alert(failureMessage);
        location.href = '/' + 'Transacao' + '/' + 'Index';
    },

    showDivAlert: function (show, mensagem, alertComponent) {
        let alert = $("#" + alertComponent);

        if (alert.length > 0) {
            alert.text(mensagem);
            if (show) {
                alert.show();
            } else {
                alert.hide();
            }
        }
    }


}


