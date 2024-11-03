import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Categories } from 'src/app/entities/categories';
import { SavePrepTestConfig } from 'src/app/entities/preptestconfig';
import { CategoriesService } from 'src/app/services/categories.service';
import { ContextService } from 'src/app/services/context.service';
import { PrepTestService } from 'src/app/services/prep-test.service';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { BasePopupComponent } from 'src/app/shared/base-popup/base-popup.component';
import { PageAccessType, Roles } from 'src/app/shared/enums';

@Component({
  selector: 'app-prep-test-config.component',
  templateUrl: './prep-test-config.component.html',
})
export class PrepTestConfigComponent extends BasePopupComponent  implements OnInit {
  public submitted = false;
  private _parent: Categories
  public isPrepTestConfigInProgress: boolean = false;
  public isEdit:boolean =false;
  public listCategories: any = [];
  allSelected = false;
  public prepTestConfigForm = this._formBuilder.group({
    id: [0, [Validators.required]], // Default to 0 or whatever makes sense
    name: ['', [Validators.required, Validators.maxLength(200)]],
    timeBox: [0, [Validators.required, Validators.min(1)]],
    totalQuestions: [0, [Validators.required, Validators.min(1)]],
    // unAttemptQuestions: [true],
    // wrongAnswers: [true],
    // allQuestions: [true],
    resultEnd: [true],
    questionCriteria: ['all'], // Default option (can be 'unAttempted', 'wrong', or 'all')
    categories: [[], [Validators.required]] // Add categories as an array
});


  constructor(
    protected _dialogRef: MatDialogRef<PrepTestConfigComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    protected _contextService: ContextService,
    protected _router: Router,
    private _prepTestService: PrepTestService,
    private _formBuilder: FormBuilder,
    private toastService: ToastNotificationService,
    private _categoryService: CategoriesService,

  ) {
    super(_contextService, _dialogRef,_router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    
    this._parent = _data.parent;
   }

  ngOnInit() {
    this.getAllDropDownCategories();
  }

  get myprepTestConfigForm() {
    return this.prepTestConfigForm.controls;
}

insertPrepTestConfig(config: SavePrepTestConfig) {
  this.isPrepTestConfigInProgress = true;
  this._prepTestService.insertPrepTestConfig(config).subscribe((data: number) => {
    if(data == -1)
    {
      this.toastService.showError("Category should be unique.", "Category");

    }
    else
    {
      this.toastService.showSuccess("Category is saved successfully.", "Category");

    }
   
    this._dialogRef.close(true);
  
    this.isPrepTestConfigInProgress = false;
    
  })

}
savePrepTestConfig() {
  this.submitted = true;
  if (!this.prepTestConfigForm.valid) {
      return false;
  } else {
      // Create a new Questions instance using the categoryForm value
      const config = new SavePrepTestConfig({
          id: this.prepTestConfigForm.value.id, // Ensure id is passed correctly
          name: this.prepTestConfigForm.value.name,
          timeBox: this.prepTestConfigForm.value.timeBox,
          totalQuestions: this.prepTestConfigForm.value.totalQuestions,
          // unAttemptQuestions: this.prepTestConfigForm.value.unAttemptQuestions,
          // wrongAnswers: this.prepTestConfigForm.value.wrongAnswers,
          // allQuestions: this.prepTestConfigForm.value.allQuestions,
          questionCriteria: this.prepTestConfigForm.value.questionCriteria,
          resultEnd: this.prepTestConfigForm.value.resultEnd,
          categories: this.prepTestConfigForm.value.categories,

      });
      this.insertPrepTestConfig(config);
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

toggleSelectAll(event: Event) {
  event.stopPropagation(); // Prevent the select-all click from closing the dropdown
  this.allSelected = !this.allSelected;

  if (this.allSelected) {
    // Select all categories
    const allCategoryIds = this.listCategories.map(category => category.id);
    // this.prepTestConfigForm.controls.categories.setValue(allCategoryIds);
    this.prepTestConfigForm.patchValue({
      categories: allCategoryIds
  });
  } else {
    // Deselect all categories
    this.prepTestConfigForm.patchValue({
      categories: null
  });
    // this.prepTestConfigForm.controls.categories.setValue(null);
  }
}

onCategorySelectionChange() {
  const selectedCategories = this.prepTestConfigForm.controls.categories.value || [];
  this.allSelected = selectedCategories.length === this.listCategories.length;
}

}