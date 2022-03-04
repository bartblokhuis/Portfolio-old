declare var $: any;

export function validateLanguageForm(form: any) {
    form.validate({
        rules: {
          name: {
            required: true,
            maxlength: 64
          },
          languageCulture: {
            maxlength: 12
          },
          metaTitle: {
            maxlength: 256
          },
          flagImageFilePath: {
            accept: "image/jpeg, image/pjpeg, image/png, image/svg+xml, image/tiff, image/webp",
            required: true
          }
        },
        messages: { 
          name: {
            maxlength: "Please don't use more than 64 characters in the name"
          },
          languageCulture: {
            maxlength: "Please don't use more than 12 characters in the language culture"
          },
          flagImageFilePath: {
            accept: "Please use an image file",
            required: "Please enter the language flag icon"
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