
declare var $: any;

export function validateSkillForm(form: any) {
    form.validate({
        rules: {
          name: {
            required: true,
          },
          file: {
            accept: "image/jpeg, image/pjpeg, image/png, image/svg+xml, image/tiff, image/webp"
          }
        },
        messages: {
          name: {
            required: "Please enter a title",
          },
          file: {
            accept: "Please enter an image"
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

