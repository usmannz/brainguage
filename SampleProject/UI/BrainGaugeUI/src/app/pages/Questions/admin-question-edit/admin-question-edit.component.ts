import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Questions } from 'src/app/entities/questions';
import { CategoriesService } from 'src/app/services/categories.service';
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
  public listCategories: any = [];
  selectedFile: File | null = null;
  imageUrl: any; // To hold the image URL
  public questionForm = this._formBuilder.group({
    id: [0, [Validators.required]], // Default to 0 or whatever makes sense
    question: ['', [Validators.required, Validators.maxLength(5000)]],
    description: ['', [Validators.required, Validators.maxLength(5000)]],
    option1: ['', [Validators.required, Validators.maxLength(5000)]],
    option2: ['', [Validators.required, Validators.maxLength(5000)]],
    option3: ['', [Validators.required, Validators.maxLength(5000)]],
    option4: ['', [Validators.required, Validators.maxLength(5000)]],
    option5: ['', [Validators.required, Validators.maxLength(5000)]],
    isMockExam: [true],
    isDemo: [true],
    categoriesId: [0, [Validators.required, Validators.min(1)]],
    file: [],
    correctAnswer: [1, [Validators.required]]
  });


  constructor(
    protected _dialogRef: MatDialogRef<AdminQuestionEditComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    protected _contextService: ContextService,
    protected _router: Router,
    private _questionService: QuestionService,
    private _categoryService: CategoriesService,
    private _formBuilder: FormBuilder,
    private toastService: ToastNotificationService,
    private cd: ChangeDetectorRef
  ) {
    super(_contextService, _dialogRef,_router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    
    this._parent = _data.parent;
   }

  ngOnInit() {
    this.getAllDropDownCategories();
    if (this._data.question != null) {
      this.isEdit = true;
      this.questionForm.patchValue(this._data.question);
      this.imageUrl =this._data.question?.pictureWebPath;
      console.log(this._data.question)
    }
  }

  get myQuestionForm() {
    return this.questionForm.controls;
}

insertQuestion(question: Questions) {
  this.isQuestionInProgress = true;
  // const formData = new FormData();

  // Object.keys(this.questionForm.controls).forEach(key => {
  //   formData.append(key, this.questionForm.get(key)?.value);
  // });

  // formData.append('file', this.selectedFile);
  const formData = new FormData();
  formData.append('id', this.questionForm.get('id')?.value.toString()); // Convert to string
  formData.append('question', this.questionForm.get('question')?.value);
  formData.append('description', this.questionForm.get('description')?.value);
  formData.append('option1', this.questionForm.get('option1')?.value);
  formData.append('option2', this.questionForm.get('option2')?.value);
  formData.append('option3', this.questionForm.get('option3')?.value);
  formData.append('option4', this.questionForm.get('option4')?.value);
  formData.append('option5', this.questionForm.get('option5')?.value);
  formData.append('isMockExam', this.questionForm.get('isMockExam')?.value.toString());
  formData.append('isDemo', this.questionForm.get('isDemo')?.value.toString());
  formData.append('categoriesId', this.questionForm.get('categoriesId')?.value.toString());
  formData.append('correctAnswer', this.questionForm.get('correctAnswer')?.value.toString());

  if (this.selectedFile) {
    formData.append('file', this.selectedFile);
  }  
  this._questionService.insertQuestion(formData).subscribe((data: number) => {
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
          description: this.questionForm.value.description,
          option1: this.questionForm.value.option1,
          option2: this.questionForm.value.option2,
          option3: this.questionForm.value.option3,
          option4: this.questionForm.value.option4,
          option5: this.questionForm.value.option5,
          isMockExam: this.questionForm.value.isMockExam,
          isDemo: this.questionForm.value.isDemo,
          categoriesId: this.questionForm.value.categoriesId,
          file: this.questionForm.value.file,
          correctAnswer : this.questionForm.value.correctAnswer

      });
      this.insertQuestion(newQuestion);
  }
}
getAllDropDownCategories() {
  this._categoryService.getAllDropDownCategories().subscribe((d: any) => {
    if(d?.data != null && d.status.code == 200)
    {
      this.listCategories = d.data;
    }
  });
}

// onFileSelected(event: any): void {
//   const file = event.target.files[0];
//   if (file) {
//     this.selectedFile = file;
//     this.questionForm.patchValue({ file: file }); // This helps to bind the file to the form
//   }
// }

onFileSelected(event: any) {
  const file = event.target.files[0];
  if (file) {
    // Manually store the file (do not patch it to the form)
    this.selectedFile = file;

    // Create a FileReader to preview the new image
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.imageUrl = e.target.result;  // Set the base64 encoded image to imageUrl
      this.cd.detectChanges();  // Trigger change detection manually to update the UI
    };
    reader.readAsDataURL(file);
  }
}


}