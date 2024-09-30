import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Questions } from 'src/app/entities/questions';
import { ContextService } from 'src/app/services/context.service';
import { QuestionService } from 'src/app/services/question.service';
import { BasePopupComponent } from 'src/app/shared/base-popup/base-popup.component';
import { PageAccessType, Roles } from 'src/app/shared/enums';

@Component({
  selector: 'app-question-edit',
  templateUrl: './question-edit.component.html',
})
export class QuestionEditComponent extends BasePopupComponent  implements OnInit {
  public submitted = false;
  private _parent: Questions
  public isLicenseInProgress: boolean = false;
  public isEdit:boolean =false;
  public licenseForm = this._formBuilder.group({
    id: [0, [Validators.required]], // Default to 0 or whatever makes sense
    question: ['', [Validators.required, Validators.maxLength(5000)]],
    answer: ['', [Validators.required, Validators.maxLength(5000)]],
});


  constructor(
    protected _dialogRef: MatDialogRef<QuestionEditComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    protected _contextService: ContextService,
    protected _router: Router,
    private _questionService: QuestionService,
    private _formBuilder: FormBuilder,
  ) {
    super(_contextService, _dialogRef,_router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    
    this._parent = _data.parent;
   }

  ngOnInit() {
    if (this._data.license != null) {
      this.isEdit = true;
      this.licenseForm.patchValue(this._data.license);
    }
  }

  get myLicenseForm() {
    return this.licenseForm.controls;
}

insertLicense(question: Questions) {
  this.isLicenseInProgress = true;
  this._questionService.insertQuestion(question).subscribe((licenseId: number) => {
    if(licenseId == -1)
    {
      this.showMessage("License name should be unique.", "warning");
    }
    else
    {
      this.showMessage("License is saved successfully.", "success");
    }
   
    this._dialogRef.close(true);
  
    this.isLicenseInProgress = false;
    
  })

}
saveLicense() {
  this.submitted = true;
  if (!this.licenseForm.valid) {
      return false;
  } else {
      // Create a new Questions instance using the licenseForm value
      const newQuestion = new Questions({
          id: this.licenseForm.value.id, // Ensure id is passed correctly
          question: this.licenseForm.value.question,
          answer: this.licenseForm.value.answer
      });
      this.insertLicense(newQuestion);
  }
}


}