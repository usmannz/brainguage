import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Questions } from 'src/app/entities/questions';
import { ContextService } from 'src/app/services/context.service';
import { QuestionService } from 'src/app/services/question.service';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { BasePopupComponent } from 'src/app/shared/base-popup/base-popup.component';
import { PageAccessType, Roles } from 'src/app/shared/enums';

@Component({
  selector: 'app-admin-question-edit.component',
  templateUrl: './admin-question-edit.component.html',
})
export class AdminQuestionEditComponent extends BasePopupComponent  implements OnInit {
  public submitted = false;
  private _parent: Questions
  public isQuestionInProgress: boolean = false;
  public isEdit:boolean =false;
  public questionForm = this._formBuilder.group({
    id: [0, [Validators.required]], // Default to 0 or whatever makes sense
    question: ['', [Validators.required, Validators.maxLength(5000)]],
    answer: ['', [Validators.required, Validators.maxLength(5000)]],
});


  constructor(
    protected _dialogRef: MatDialogRef<AdminQuestionEditComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    protected _contextService: ContextService,
    protected _router: Router,
    private _questionService: QuestionService,
    private _formBuilder: FormBuilder,
    private toastService: ToastNotificationService
  ) {
    super(_contextService, _dialogRef,_router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    
    this._parent = _data.parent;
   }

  ngOnInit() {
    if (this._data.question != null) {
      this.isEdit = true;
      this.questionForm.patchValue(this._data.question);
    }
  }

  get myQuestionForm() {
    return this.questionForm.controls;
}

insertQuestion(question: Questions) {
  this.isQuestionInProgress = true;
  this._questionService.insertQuestion(question).subscribe((data: number) => {
    if(data == -1)
    {
      this.toastService.showError("Question should be unique.", "Question");

    }
    else
    {
      this.toastService.showSuccess("Question is saved successfully.", "Question");

    }
   
    this._dialogRef.close(true);
  
    this.isQuestionInProgress = false;
    
  })

}
saveQuestion() {
  this.submitted = true;
  if (!this.questionForm.valid) {
      return false;
  } else {
      // Create a new Questions instance using the questionForm value
      const newQuestion = new Questions({
          id: this.questionForm.value.id, // Ensure id is passed correctly
          question: this.questionForm.value.question,
          answer: this.questionForm.value.answer
      });
      this.insertQuestion(newQuestion);
  }
}


}