import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { ContextService } from 'src/app/services/context.service';
import { Router } from '@angular/router';
import { PageAccessType, Roles, SortDirection, SortFields } from 'src/app/shared/enums';
import { Pager } from 'src/app/entities/pager';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDialog } from '@angular/material/dialog';
import { Helper } from 'src/app/helpers/helper';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { CategoryEditComponent } from '../categories-edit/category-edit.component';
import { Categories } from 'src/app/entities/categories';

@Component({
  selector: 'app-categories-listing',
  templateUrl: './categories-listing.component.html',
  styleUrls: ['./categories-listing.component.scss'],
})
export class CategoriesListingComponent extends BaseComponent  implements OnInit {

  public sortByField: typeof SortFields = SortFields;
  private _sortDirection: typeof SortDirection = SortDirection;
  public sortBy: number = this.sortByField.CreateStamp;
  public sortDirection: boolean = true;
  public listCategories: any = [];
  panelOpenState: boolean = false;
  // _userStatus = UserStatus;
  public pagination: Pager = new Pager();
  public filterText: string = "";
  public isFilterInProgress: boolean = false;
  public count: number;
  public isAdmin: boolean = null;
  public currentTenantId:number=this._contextService.CurrentTenantId;
  constructor(
    protected _ctxService: ContextService,
    private _categoryService: CategoriesService,
   private _dialog: MatDialog,
    protected _router: Router,
    private toastService: ToastNotificationService
  ) {
    super(_ctxService, _router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    this.onClickSortBy(this.sortBy);
    this.getPageData();
  }

  ngOnInit() {
    super.ngOnInit();
    this.isAdmin = this.isAdminRole();
  }

  getPageData() {
    console.log("ca")
      this._categoryService.getAllCategories(this.pagination).subscribe((d: any) => {
        console.log(d.data)
        console.log(d.status.code)
        if(d?.data != null && d.status.code ==200)
        {
          this.listCategories = d.data;
          this.count = this.listCategories.count;
          console.log(this.listCategories,"q")
          console.log(this.count)
    
        }
        this.isFilterInProgress = false;
      });
  }

  onPageChange($event) {
    this.pagination.pageIndex = Number($event.pageIndex);
    this.pagination.pageSize = Number($event.pageSize)
    this.getPageData();

  }
  onClickSortBy(sortBy: SortFields) {
    if (this.sortBy === sortBy) {
      this.sortDirection = !this.sortDirection;
    }
    else {
      this.sortDirection = true;
    }
    this.sortBy = sortBy;
    this.pagination.sortDirection = this.sortDirection ? this._sortDirection.Asc : this._sortDirection.Desc;
    this.pagination.sortBy = sortBy;
    // this.pagination.pageIndex = 1;

    this.getPageData();
  }

  onClickCustomFilter(filterText: string) {
    this.pagination.filterText = filterText;
    this.pagination.pageIndex = 1;
    this.isFilterInProgress = true;

    this.getPageData();
  }

  clearCustomFilter() {
    this.pagination.pageIndex = 1;

    if (this.pagination.filterText) {
      this.filterText = '';
      this.pagination.filterText = '';
      this.isFilterInProgress = true;

      this.getPageData();
    }
    else {
      this.filterText = '';
    }
  }



  onchangePageSize(pageSize) {
    this.pagination.pageSize = pageSize;
    this.pagination.pageIndex = 1;

    this.getPageData();
  }

  btnAddItem_Clicked() {

    const dialogRef = this._dialog.open(CategoryEditComponent, {
      height: '500px',
      width: '900px',
      maxHeight: '500px',
      maxWidth: '900px',    
        data: { category: null, parent: this }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getPageData();
      if (result) {
      }
    });
  }

  btnEditItem_Clicked(property: Categories) {

    const dialogRef = this._dialog.open(CategoryEditComponent, {
      width: '900px',
      height: '450px',
      data: { category: property, parent: this }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getPageData();
      if (result) {
      }
    });
  }

  btnDeleteItem_Clicked(category: Categories) {
    const confirmDialog = this._dialog.open(ConfirmationDialogComponent, {
      width: '640px',
      height: '250px',
      data: {
        title: 'Confirm Delete Category',
        message: 'Are you sure you want to delete category \"' + Helper.deleteConfirmationTextWrap( category.name) + "\" " + "?"
      }
    });
    confirmDialog.afterClosed().subscribe(result => {
      if (result === true) {
        this._categoryService.deleteCategory(category.id).subscribe((d: any) => {
          if(d?.data > 0 && d.status.code ==200)
          {
          this.toastService.showSuccess("Category is deleted successfully.", "Category");
          const indexs = this.listCategories.categories.findIndex(d => d === category);
          this.listCategories.categories.splice(indexs, 1);
          if (this.listCategories.categories.length == 0) {
            if (this.pagination.pageIndex != 1) {
              this.pagination.pageIndex = this.pagination.pageIndex - 1;
            }
            this.getPageData();

          }
          else if(this.listCategories.categories.length  <=  this.pagination.pageSize && this.listCategories.count > this.listCategories.categories.length ) {
            this.getPageData();
          }
        }
        else
        {
          this.toastService.showWarning("Error.", "Category");

        }
        });
        
     }
   });
  }
}