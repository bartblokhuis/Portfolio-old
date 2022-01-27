declare var $: any;

export function validateBlogForm(form: any) {
    form.validate({
        rules: {
          title: {
            required: true,
            maxlength: 64
          },
          metaDescription: {
            maxlength: 256
          },
          metaTitle: {
            maxlength: 256
          }
        },
        messages: {
          title: {
            required: "Please enter a title",
          },
          metaDescription: {
            maxlength: "Please don't use more than 256 charachters in the meta description"
          },
          metaTitle: {
            maxlength: "Please don't use more than 256 charachter in the meta title"
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