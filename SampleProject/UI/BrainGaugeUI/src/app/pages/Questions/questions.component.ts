import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { ContextService } from 'src/app/services/context.service';
import { Router } from '@angular/router';
import { PageAccessType, Roles, SortDirection, SortFields } from 'src/app/shared/enums';
import { QuestionService } from 'src/app/services/question.service';
import { Pager } from 'src/app/entities/pager';
import { MatExpansionModule } from '@angular/material/expansion';
import { QuestionEditComponent } from '../question-edit/question-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { Questions } from 'src/app/entities/questions';
import { Helper } from 'src/app/helpers/helper';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.scss'],
})
export class QuestionsComponent extends BaseComponent  implements OnInit {

  public sortByField: typeof SortFields = SortFields;
  private _sortDirection: typeof SortDirection = SortDirection;
  public sortBy: number = this.sortByField.CreateStamp;
  public sortDirection: boolean = true;
  public listQuestions: any = [];
  panelOpenState: boolean = false;
  // _userStatus = UserStatus;
  public pagination: Pager = new Pager();
  public filterText: string = "";
  public isFilterInProgress: boolean = false;
  public count: number;
  public currentTenantId:number=this._contextService.CurrentTenantId;
  constructor(
    protected _ctxService: ContextService,
    private _questionService: QuestionService,
   private _dialog: MatDialog,
    protected _router: Router
  ) {
    super(_ctxService, _router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    this.onClickSortBy(this.sortBy);
    this.getPageData();
  }

  ngOnInit() {
    super.ngOnInit();
  }

  getPageData() {
    this._questionService.getAllQuestions(this.pagination).subscribe((d: any) => {
      console.log(d.data)
      console.log(d.status.code)
      if(d?.data != null && d.status.code ==200)
      {
        this.listQuestions = d.data;
        this.count = this.listQuestions.count;
        console.log(this.listQuestions,"q")
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

    const dialogRef = this._dialog.open(QuestionEditComponent, {
      height: '500px',
      width: '900px',
      maxHeight: '500px',
      maxWidth: '900px',    
        data: { license: null, parent: this }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getPageData();
      if (result) {
      }
    });
  }

  btnEditItem_Clicked(property: Questions) {

    const dialogRef = this._dialog.open(QuestionEditComponent, {
      width: '900px',
      height: '450px',
      data: { license: property, parent: this }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getPageData();
      if (result) {
      }
    });
  }

  btnDeleteItem_Clicked(license: Questions) {
    const confirmDialog = this._dialog.open(ConfirmationDialogComponent, {
      width: '540px',
      height: '210px',
      data: {
        title: 'Confirm Delete License',
        message: 'Are you sure you want to delete license \"' + Helper.deleteConfirmationTextWrap( license.question) + "\" " + "?"
      }
    });
    confirmDialog.afterClosed().subscribe(result => {
      if (result === true) {
        this._questionService.deleteQuestion(license.id).subscribe((license: any) => {
          if(license)
          {
          this.showMessage("License is deleted successfully.", "success");
          const indexs = this.listQuestions.license.findIndex(d => d === license);

          this.listQuestions.license.splice(indexs, 1);
          if (this.listQuestions.length == 0) {
            if (this.pagination.pageIndex != 1) {
              this.pagination.pageIndex = this.pagination.pageIndex - 1;
            }
            this.getPageData();

          }
          else if(this.listQuestions.license.length  <=  this.pagination.pageSize/2 && this.listQuestions.count > this.listQuestions.license.length ) {
            this.getPageData();

          }
        }
        });
        
     }
   });
  }
}