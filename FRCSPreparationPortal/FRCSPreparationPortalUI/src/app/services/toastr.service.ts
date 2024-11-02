import { Injectable } from '@angular/core';
import { ToastrService as NgxToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastNotificationService {

  constructor(private toastr: NgxToastrService) {}

  showSuccess(message: string, title?: string) {
    this.toastr.success(message, "");
  }

  showError(message: string, title?: string) {
    this.toastr.error(message, "");
  }

  showInfo(message: string, title?: string) {
    this.toastr.info(message, "");
  }

  showWarning(message: string, title?: string) {
    this.toastr.warning(message, "");
  }
}
