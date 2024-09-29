import { Injectable } from '@angular/core';
import { ToastrService as NgxToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastNotificationService {

  constructor(private toastr: NgxToastrService) {}

  showSuccess(message: string, title?: string) {
    this.toastr.success(message, title);
  }

  showError(message: string, title?: string) {
    console.log("asd")
    this.toastr.error(message, title);
  }

  showInfo(message: string, title?: string) {
    this.toastr.info(message, title);
  }

  showWarning(message: string, title?: string) {
    this.toastr.warning(message, title);
  }
}
