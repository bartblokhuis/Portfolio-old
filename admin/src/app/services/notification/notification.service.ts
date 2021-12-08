import { Injectable } from '@angular/core';

declare var Swal: any;

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { }

  private toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast: any) => {
      toast.addEventListener('mouseenter', Swal.stopTimer)
      toast.addEventListener('mouseleave', Swal.resumeTimer)
      toast.addEventListener('click', Swal.close)
    }
  });

  success(content: string): void {
    this.toast.fire({
      icon: 'success',
      title: content
    })
  }

  info(content: string): void {
    this.toast.fire({
      icon: 'info',
      title: content
    })
  }

  error(content: string): void {
    this.toast.fire({
      icon: 'error',
      title: content
    })
  }

  warning(content: string): void {
    this.toast.fire({
      icon: 'warning',
      title: content
    })
  }

  question(content: string): void {
    this.toast.fire({
      icon: 'question',
      title: content
    })
  }

}
