declare var $: any;

export function validateBlogForm(form: any) {
    form.validate({
        rules: {
          title: {
            required: true,
          }
        },
        messages: {
          title: {
            required: "Please enter a title",
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