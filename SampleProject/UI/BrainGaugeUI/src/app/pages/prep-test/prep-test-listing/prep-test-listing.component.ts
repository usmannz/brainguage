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
import { Categories } from 'src/app/entities/categories';
import { PrepTestService } from 'src/app/services/prep-test.service';
import { PrepTestConfigComponent } from '../prep-test-config/prep-test-config.component';

@Component({
  selector: 'app-prep-test-listing',
  templateUrl: './prep-test-listing.component.html',
  styleUrls: ['./prep-test-listing.component.scss'],
})
export class PrepTestListingComponent extends BaseComponent  implements OnInit {

  public sortByField: typeof SortFields = SortFields;
  private _sortDirection: typeof SortDirection = SortDirection;
  public sortBy: number = this.sortByField.CreateStamp;
  public sortDirection: boolean = true;
  public listPrepTest: any = [];
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
    private _prepTestService: PrepTestService,
   private _dialog: MatDialog,
    protected _router: Router,
    private toastService: ToastNotificationService
  ) {
    super(_ctxService, _router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.User);
    this.onClickSortBy(this.sortBy);
    this.getPageData();
  }

  ngOnInit() {
    super.ngOnInit();
    this.isAdmin = this.isAdminRole();
  }

  getPageData() {
    console.log("ca")
      this._prepTestService.getAllPrepTests(this.pagination).subscribe((d: any) => {
        if(d?.data != null && d.status.code ==200)
        {
          this.listPrepTest = d.data;
          this.count = this.listPrepTest.count;
          console.log(this.listPrepTest,"q")
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
    const dialogRef = this._dialog.open(PrepTestConfigComponent, {
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

  routeToPrepTest(prepTestConfigId: number) {
    var url = `prep-test/${prepTestConfigId}}`
   this._router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
     this._router.navigate([url]));

  }
}