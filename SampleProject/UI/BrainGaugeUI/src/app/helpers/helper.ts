import { HttpClient } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import * as moment from 'moment';
import { SideMenu } from '../shared/enums';

export class Helper {
    constructor(private _http: HttpClient) { }
    static _seoString;
    public date = new Date();
    public static menuItemsDisplay = [
        {
            name: SideMenu.Dashboard,
            show: false
        },
        {
            name: SideMenu.Questions,
            show: false
        },
        {
            name: SideMenu.Admin,
            show: false
        },

        {
            name: SideMenu.User,
            show: false
        }
    ];

   static getCurrentDateUTC(): string {
        const currentDate = new Date();
        return currentDate.toISOString().split('T')[0] + 'T00:00:00';
      }

    // base64ToArrayBuffer /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static base64ToArrayBuffer(base64): any {
        var binaryString = window.atob(base64);
        var len = binaryString.length;
        var bytes = new Uint8Array(len);
        for (var i = 0; i < len; i++) {
            bytes[i] = binaryString.charCodeAt(i);
        }
        return bytes.buffer;
    }

    // parseJwt /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static parseJwt(token) {
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace('-', '+').replace('_', '/');
        return JSON.parse(window.atob(base64));
    };

    // custom validator to check that two fields match
    static MustMatch(controlName: string, matchingControlName: string) {
        return (formGroup: FormGroup) => {
            const control = formGroup.controls[controlName];
            const matchingControl = formGroup.controls[matchingControlName];

            if (matchingControl.errors && !matchingControl.errors.mustMatch) {
                // return if another validator has already found an error on the matchingControl
                return;
            }
            // set error on matchingControl if validation fails
            if (control.value !== matchingControl.value) {
                matchingControl.setErrors({ mustMatch: true });
            } else {
                matchingControl.setErrors(null);
            }
        }
    }
     // custom validator to check that two fields match
     static CompareDate(startDate: string, endDate: string) {
        return (formGroup: FormGroup) => {
            const start = formGroup.controls[startDate];
            const end = formGroup.controls[endDate];

            if (end.errors && !end.errors.isLater) {
                // return if another validator has already found an error on the matchingControl
                return;
            }
            // set error on matchingControl if validation fails
            if (start.value >= end.value) {
                end.setErrors({ isLater: true });
            } else {
                end.setErrors(null);
            }
        }
    }

         // custom validator to check that two fields match
         static ValidateStartDate(startDate: string, endDate: string) {
            return (formGroup: FormGroup) => {
                const start = formGroup.controls[startDate];
                const end = formGroup.controls[endDate];
    
                if (start.errors && !start.errors.isLater) {
                    // return if another validator has already found an error on the matchingControl
                    return;
                }
                // set error on matchingControl if validation fails
                if (start.value > end.value) {
                    start.setErrors({ isLater: true });
                } else {
                    start.setErrors(null);
                }
            }
        }

    static WorkEmail(email: string) {     
        var regex = '^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(?!hotmail|gmail|yahoo)(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$';
        var emailPattern = new RegExp(regex);
        
        return (formGroup: FormGroup) => {
            const control = formGroup.controls[email];
            if (control.errors && !control.errors.mustMatch) {
                // return if another validator has already found an error on the matchingControl
                return;
            }
            // set error on matchingControl if validation fails
            if (emailPattern.test(control.value)) {
                control.setErrors({ mustMatch: true });
            } else {
                control.setErrors(null);
            }
        }
    }

     // a and b are javascript Date objects
    static dateDiffInDays(a, b) {
    const _MS_PER_DAY = 1000 * 60 * 60 * 24;
    // Discard the time and time-zone information.
    const utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
    const utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());
  
    return Math.floor((utc1-utc2) / _MS_PER_DAY);
  }

  static deleteConfirmationTextWrap(text: string): string {
    if (text.length > 100) {
      return text.substring(0, 100) + '...';
    }
    return text;
  }
  
  static capitalizeWords(input: string): string {
    if (!input) return input;

    return input
      .toLowerCase() // Convert the entire string to lowercase
      .split(' ')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');
  }

  static JoinWords(input: string): string {
    if (!input) return input;

    return input
      .toLowerCase() // Convert the entire string to lowercase
      .split(' ')
      .map(word => word.charAt(0).toLowerCase() + word.slice(1))
      .join('-');
  }
    
}