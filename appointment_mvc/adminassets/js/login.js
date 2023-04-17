
$("#btnlogin").click(function () {

    if ($("#username").val().trim() == "" || $("#username").val().trim().indexOf("\"") > -1 || $("#username").val().trim().indexOf("'") > -1) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please Enter user name Correctly!',
            footer: 'JetSet',
            allowOutsideClick: false,
            allowEscapeKey: false,
            allowEnterKey: false,
        }).then((result) => {
            $("#email").focus();
        });
        return false;
    }


    if ($("#password").val().trim() == "" || $("#password").val().trim().indexOf("\"") > -1 || $("#password").val().trim().indexOf("'") > -1) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please Enter Password Correctly!',
            footer: 'JetSet',
            allowOutsideClick: false,
            allowEscapeKey: false,
            allowEnterKey: false,
        }).then((result) => {
            $("#password").focus();
        });
        return false;
    }
    var formdata = $('#Login').serialize();

    console.log(formdata);

    Swal.fire({
        title: 'Please wait!',
        html: 'We are validating your details',
        timer: 200000000,
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        didOpen: () => {
            Swal.showLoading()
        },
        willClose: () => {
            clearInterval(timerInterval)
        }
    });



    $.ajax({
        async: true,
        url: "/home/Login",
        type: 'POST',
        data: formdata,
        headers: { "Accept": "application/json" },

        processData: false,
        beforeSend: function () {
        },
        success: function (data, textStatus, xhr) {
            console.log(data);
            if (data.success == true) {

                window.location.href = data.message;
            }
            else {
                Swal.fire({
                    icon: 'error',
                    html: '<p style="font-size: 17px;">' + data.message + '</p>',
                    returnFocus: true,
                    returnFocus: false,
                }).then((result) => {
                    if (result.isConfirmed) {

                        return false;
                    }
                })
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            Swal.fire({
                icon: 'error',
                html: '<p style="font-size: 17px;">An Error Occured try again later or contact website administration.</p>',
                returnFocus: true,
                returnFocus: false,
            }).then((result) => {
                if (result.isConfirmed) {

                    return false;
                }
            })

        },
        complete: function () {
        }
    });

    return false;

});