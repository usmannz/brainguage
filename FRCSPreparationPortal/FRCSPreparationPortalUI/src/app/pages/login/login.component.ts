import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie';
import { User } from 'src/app/entities/user';
import { Helper } from 'src/app/helpers/helper';
import { Settings } from 'src/app/helpers/settings';
import { ContextService } from 'src/app/services/context.service';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { UserService } from 'src/app/services/user.service';
import { BaseComponent } from 'src/app/shared/base/base.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {
  public submitted = false;
  public isLoginInProgress: boolean = false;
  private _returnUrl: string;

  public loginForm = this._formBuilder.group({
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      rememberMe: [false]
  });
   private _now = new Date();
  private _exp =  new Date(this._now.getFullYear()+1, this._now.getMonth(), this._now.getDate());
  constructor(
      protected _contextService: ContextService,
      private _userService: UserService,
      protected _router: Router,
      private _formBuilder: FormBuilder,
      private _cookieService: CookieService,
      protected _route: ActivatedRoute,
      private toastService: ToastNotificationService

  ) {
      super(_contextService,_router);
      if(this._cookieService.get('emailAddress'))
      {
      this.loginForm.patchValue({
          emailAddress: this._cookieService.get('emailAddress'),
          rememberMe:  this._cookieService.get('remember') === "true"
      });
      }
  }

  ngOnInit() {
      this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }

  get myLoginForm() {
      return this.loginForm.controls;
  }

  btnLogin_Clicked() {
      this.submitted = true;
      if (!this.loginForm.valid) {
          return false;
      } else {

          var user = new User({ email: this.loginForm.value.emailAddress.trim(), password: this.loginForm.value.password.trim() });
          this.isLoginInProgress = true;

          this._userService.authenticate(user).subscribe(d => {
              this.isLoginInProgress = false;
          if(d.success == 1)
          {
              localStorage["app"] = d.jwt;
              localStorage["role"] =JSON.stringify( d.role);
              localStorage["email"]=JSON.stringify( user.email); 
              localStorage["isEmail"]=JSON.stringify( d.isEmail); 
              this._contextService._token = d.jwt;
              this._contextService._session = Helper.parseJwt(d.jwt);
              this._contextService._userRoles = d.role;
              if (this.loginForm.value.rememberMe) {
                  this._cookieService.put('emailAddress', this.loginForm.value.emailAddress,{  expires: this._exp});
                  this._cookieService.put('remember', String(this.loginForm.value.rememberMe),{  expires: this._exp});

              }
              else {
                  this._cookieService.remove('emailAddress'),
                  this._cookieService.remove('remember')
              }
              if(this._returnUrl == "/")
              {
                if(this.isAdminRole())
                {
                    this._router.navigate(['/questions'])
                }
                else
                {
                    this._router.navigate(['/demo-test'])
                }
              }
              else
              {
                this._router.navigate([this._returnUrl]);
              }
          }
          else if (d.success == -1 )
          {
              this.toastService.showError("Organization is inactive.", "error");
          }
          else if (d.success == -2)
          {
            this.toastService.showError("User is inactive.", "error");
          }
          }, err => {
              this.isLoginInProgress = false;
              switch (err.status) {
                  case 401:
                    this.toastService.showError("Invalid user name or password.", "error");
                      break;
                  default:
                    this.toastService.showWarning("Something went wrong, please try again.", "error");
                      break;
              }
          });
      }
  }

   ngOnDestroy() {
  }

}
