declare var $: any;

export function validateScheduleTaskForm(form: any) {
    form.validate({
        rules: {
          name: {
            required: true,
            maxlength: 128
          },
          seconds: {
            required: true,
            min: 1,
            max: 2147483647,
          },
          type: {
            required: true
          }
        },
        messages: {
          name: {
            required: "Please enter the task name",
            maxlength: "Please don't enter a name with more than 128 character"
          },
          seconds: {
            required: "Please enter the tasks tun time",
            min: "The run time can't be less than one second",
            max: "The run time can't be longer than 2147483647 seconds",
          },
          type: {
            required: "Please enter the task type"
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