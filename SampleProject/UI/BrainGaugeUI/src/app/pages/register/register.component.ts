import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SignUp } from 'src/app/entities/user';
import { Helper } from 'src/app/helpers/helper';
import { ContextService } from 'src/app/services/context.service';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { UserService } from 'src/app/services/user.service';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { PageAccessType } from 'src/app/shared/enums';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent implements OnInit {

  public submitted = false;
  public isSignUpInProgress: boolean = false;
  public signUpForm = this._formBuilder.group({
    id: [0, [Validators.required,]],
    firstName: ['', [Validators.required,Validators.maxLength(450)]],
    lastName: ['', [Validators.required,Validators.maxLength(450)]],
    password: ['', [Validators.required,Validators.maxLength(200),
      Validators.pattern('(?=.*[A-Za-z])(?=.*[0-9])(?=.*[$@$!#^~%*?&,.<>"\'\\;:\{\\\}\\\[\\\]\\\|\\\+\\\-\\\=\\\_\\\)\\\(\\\)\\\`\\\/\\\\\\]])[A-Za-z0-9\d$@].{7,}')
    ]],
    confirm: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email,Validators.maxLength(200)]]  }
  ,
  {
    validator: Helper.MustMatch('password', 'confirm') 
  });

  constructor(
    protected _contextService: ContextService,
    protected _router: Router,
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private toastService: ToastNotificationService
  ) {
    super(_contextService, _router);
    this._pageAccessType = PageAccessType.PUBLIC;    
   }

  ngOnInit() {

  }

  get mysignUpForm() {
    return this.signUpForm.controls;
}

signUpUser(signUp: SignUp) {

  this.isSignUpInProgress = true;
  

  this._userService.signUpUser(signUp).subscribe((d: any) => {
    this.isSignUpInProgress = false;
    if(d?.data > 0 && d.status.code ==200)
        {
          this.toastService.showSuccess("Your Account has been created.", "success");
  }
  else
  {
    this.toastService.showWarning("Email Address already registered.", "warning");
  }
  })

}

signUpValidation() {
  this.submitted = true;
  if (!this.signUpForm.valid) {
    return false;
  }
  else {
    this.signUpUser(this.signUpForm.value);
  }

}

}
