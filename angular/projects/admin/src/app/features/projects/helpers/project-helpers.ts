import { environment } from "projects/admin/src/environments/environment";

declare var $: any;

export function validateProjectForm(form: any) {
    form.validate({
        rules: {
          title: {
            required: true,
          },
          file: {
            accept: "image/jpeg, image/pjpeg, image/png, image/svg+xml, image/tiff, image/webp"
          }
        },
        messages: {
          title: {
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

export function formatProjectSkillsSelect (state: any) {
  
    if (!state.id) return state.text;
    
    let src = state.element.children[0]?.id;
    if(!src){
      return state.text;
    }

    if(src === 'null') return $('<span>' + state.text + '</span>');

    src = environment.baseApiUrl + src;
    return $('<span><img src="'+ src +'" style="max-height: 20px" /> ' + state.text + '</span>');
};