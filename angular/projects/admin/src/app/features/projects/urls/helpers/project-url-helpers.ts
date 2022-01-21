declare var $: any;

export function validateUrlForm(form: any) {
    form.validate({
        rules: {
          fullUrl: {
            required: true,
            url: true,
          },
        },
        messages: {
            fullUrl: {
            required: "Please enter a url",
            url: "Please enter a real url",
          }
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