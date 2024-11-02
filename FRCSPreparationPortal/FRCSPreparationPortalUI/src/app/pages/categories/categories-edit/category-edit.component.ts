import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Categories } from 'src/app/entities/categories';
import { CategoriesService } from 'src/app/services/categories.service';
import { ContextService } from 'src/app/services/context.service';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { BasePopupComponent } from 'src/app/shared/base-popup/base-popup.component';
import { PageAccessType, Roles } from 'src/app/shared/enums';

@Component({
  selector: 'app-category-edit.component',
  templateUrl: './category-edit.component.html',
})
export class CategoryEditComponent extends BasePopupComponent  implements OnInit {
  public submitted = false;
  private _parent: Categories
  public isCategoryInProgress: boolean = false;
  public isEdit:boolean =false;
  public categoryForm = this._formBuilder.group({
    id: [0, [Validators.required]], // Default to 0 or whatever makes sense
    name: ['', [Validators.required, Validators.maxLength(5000)]],
});


  constructor(
    protected _dialogRef: MatDialogRef<CategoryEditComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    protected _contextService: ContextService,
    protected _router: Router,
    private _categoryService: CategoriesService,
    private _formBuilder: FormBuilder,
    private toastService: ToastNotificationService
  ) {
    super(_contextService, _dialogRef,_router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    
    this._parent = _data.parent;
   }

  ngOnInit() {
    if (this._data.category != null) {
      this.isEdit = true;
      this.categoryForm.patchValue(this._data.category);
    }
  }

  get mycategoryForm() {
    return this.categoryForm.controls;
}

insertCategory(category: Categories) {
  this.isCategoryInProgress = true;
  this._categoryService.insertCategory(category).subscribe((d: any) => {
    if(d?.data != null && d.status.code ==200)
    {
      if(d?.data == -1)
        {
          this.toastService.showError("Category name should be unique.", "Category");
    
        }
        else
        {
          this.toastService.showSuccess("Category is saved successfully.", "Category");
    
        }
    }
    this._dialogRef.close(true);
  
    this.isCategoryInProgress = false;
    
  })

}
saveCategory() {
  this.submitted = true;
  if (!this.categoryForm.valid) {
      return false;
  } else {
      // Create a new Questions instance using the categoryForm value
      const newCategory = new Categories({
          id: this.categoryForm.value.id, // Ensure id is passed correctly
          name: this.categoryForm.value.name,
      });
      this.insertCategory(newCategory);
  }
}


}