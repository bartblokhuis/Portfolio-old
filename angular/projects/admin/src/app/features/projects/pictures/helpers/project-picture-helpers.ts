declare var $: any;

export function validateProjectPictureForm(form: any) {
    form.validate({
        rules: {
            displayNumber: {
                required: true,
            },
        },
        messages: {
            displayNumber: {
                required: "Please enter a display number",
            },
        },
        errorElement: 'span',
        errorPlacement: function (error: any, element: any) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element: any, errorClass: any, validClass: any) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element: any, errorClass: any, validClass: any) {
            $(element).removeClass('is-invalid');
        }
    });
}