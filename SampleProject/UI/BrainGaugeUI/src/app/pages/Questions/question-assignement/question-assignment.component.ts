import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { ContextService } from 'src/app/services/context.service';
import { Router } from '@angular/router';
import { PageAccessType, Roles, SortDirection, SortFields } from 'src/app/shared/enums';
import { QuestionService } from 'src/app/services/question.service';
import { Pager } from 'src/app/entities/pager';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDialog } from '@angular/material/dialog';
import { Questions, QuestionsAssignment } from 'src/app/entities/questions';
import { Helper } from 'src/app/helpers/helper';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { AdminQuestionEditComponent } from '../admin-question-edit/admin-question-edit.component';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-question-assignment',
  templateUrl: './question-assignment.component.html',
  styleUrls: ['./question-assignment.component.scss'],
})
export class QuestionAssignmentComponent extends BaseComponent  implements OnInit {

  public sortByField: typeof SortFields = SortFields;
  private _sortDirection: typeof SortDirection = SortDirection;
  public sortBy: number = this.sortByField.CreateStamp;
  public sortDirection: boolean = true;
  public listQuestions: any = [];
  panelOpenState: boolean = false;
  // _userStatus = UserStatus;
  public pagination: Pager = new Pager();
  public filterText: string = "";
  public isQuestionInProgress: boolean = false;
  public count: number;
  public currentTenantId:number=this._contextService.CurrentTenantId;
  public listUsers: any = [];
  filteredOptions: any[] = [];
  selectedUser: any;
  selectedAssignments = new Map<number, boolean>(); // To store question ID and its selected status across pages

  constructor(
    protected _ctxService: ContextService,
    private _questionService: QuestionService,
    private _userService: UserService,
   private _dialog: MatDialog,
    protected _router: Router,
    private toastService: ToastNotificationService
  ) {
    super(_ctxService, _router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.Admin);
    this.onClickSortBy(this.sortBy);
    this.getAllDropDownUsers();
    // this.getPageData();
  }

  ngOnInit() {
    super.ngOnInit();

  }

  onUserChange(value: any) {
    if (typeof value === 'string') {

    const filterValue = value?.toLowerCase();
    this.filteredOptions = this.listUsers.filter(option =>
      `${option.firstName} ${option.lastName}`.toLowerCase().includes(filterValue)
    );
  }
  else if (value && typeof value === 'object') {
    // Handle the case where a user is selected
    this.selectedUser = value;
    if(this.selectedUser?.id > 0)
    {
      this.getPageData();
    }
  }
  }

  displayUser(user?: any): string | undefined {
    return user ? `${user.firstName} ${user.lastName}` : undefined;
  }
  
  onSelection(selectedUser: any) {
    this.selectedUser = selectedUser; // Selected user object
    console.log('Selected User ID:', this.selectedUser.Id); // Access the selected user ID here
  }

  // private _filter(value: string): string[] {
  //   const filterValue = value.toLowerCase();
  //   return this.listUsers.filter(option => option.toLowerCase().includes(filterValue));
  // }

  getAllDropDownUsers() {
    this._userService.getAllDropDownUsers().subscribe((d: any) => {
      console.log(d.data)
      console.log(d.status.code)
      if(d?.data != null && d.status.code == 200)
      {
        this.listUsers = d.data;
        this.filteredOptions = this.listUsers;

        console.log(this.listUsers,"q")  
      }
      this.isQuestionInProgress = false;
    });
  }

  getPageData() {
    console.log("ca")
    if(this.selectedUser?.id > 0)
      {  
    this._questionService.getAllUserQuestions(this.pagination,this.selectedUser.id).subscribe((d: any) => {
      console.log(d.data)
      console.log(d.status.code)
      if(d?.data != null && d.status.code ==200)
      {
        this.listQuestions = d.data;
        this.count = this.listQuestions.count;
        console.log(this.listQuestions,"q")
        console.log(this.count)
        this.listQuestions.questions.forEach(question => {
          if (this.selectedAssignments.has(question.id)) {
              question.isAssigned = this.selectedAssignments.get(question.id);
          }
      });
      }
      this.isQuestionInProgress = false;
    });
  }
  }

  onAssignmentChange(questionId: number, isAssigned: boolean) {
    this.selectedAssignments.set(questionId, isAssigned);
}

saveAssignments() {
    // Convert the selected assignments into a suitable format for saving

    if(this.selectedAssignments.size > 0)
    {
      const assignmentsToSave = Array.from(this.selectedAssignments.entries())
      .filter(([_, isAssigned]) => isAssigned)
      .map(([questionId, _]) => 
          new QuestionsAssignment({
              usersId: this.selectedUser.id, 
              questionsId: questionId
          })
      );
        this.isQuestionInProgress = true;
        this._questionService.insertUserQuestions(assignmentsToSave).subscribe((data: number) => {
          if(data == -1)
          {
            this.toastService.showError("Assignment Failed.", "Question");
      
          }
          else
          {
            this.toastService.showSuccess("Questions are assigned successfully.", "Question");
      
          }
         
        
          this.isQuestionInProgress = false;
          
        })
    }

    else
    {
      console.log('Selected User ID:', this?.selectedUser?.Id); // Access the selected user ID here
      if(!this?.selectedUser || this?.selectedUser?.id <= 0)
      {
        this.toastService.showInfo("Please Select User.", "User");
      }
      else
      {
        this.toastService.showInfo("Please Assign Questions to User.", "Question");
      }

    }

    
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
    this.isQuestionInProgress = true;

    this.getPageData();
  }

  clearCustomFilter() {
    this.pagination.pageIndex = 1;

    if (this.pagination.filterText) {
      this.filterText = '';
      this.pagination.filterText = '';
      this.isQuestionInProgress = true;

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

    const dialogRef = this._dialog.open(AdminQuestionEditComponent, {
      height: '500px',
      width: '900px',
      maxHeight: '500px',
      maxWidth: '900px',    
        data: { question: null, parent: this }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getPageData();
      if (result) {
      }
    });
  }

  btnEditItem_Clicked(property: Questions) {

    const dialogRef = this._dialog.open(AdminQuestionEditComponent, {
      width: '900px',
      height: '450px',
      data: { question: property, parent: this }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getPageData();
      if (result) {
      }
    });
  }

  btnDeleteItem_Clicked(question: Questions) {
    const confirmDialog = this._dialog.open(ConfirmationDialogComponent, {
      width: '640px',
      height: '250px',
      data: {
        title: 'Confirm Delete Question',
        message: 'Are you sure you want to delete question \"' + Helper.deleteConfirmationTextWrap( question.question) + "\" " + "?"
      }
    });
    confirmDialog.afterClosed().subscribe(result => {
      if (result === true) {
        this._questionService.deleteQuestion(question.id).subscribe((d: any) => {
          if(d?.data > 0 && d.status.code ==200)
          {
          this.toastService.showSuccess("Question is deleted successfully.", "Question");
          const indexs = this.listQuestions.questions.findIndex(d => d === question);
          this.listQuestions.questions.splice(indexs, 1);
          if (this.listQuestions.questions.length == 0) {
            if (this.pagination.pageIndex != 1) {
              this.pagination.pageIndex = this.pagination.pageIndex - 1;
            }
            this.getPageData();

          }
          else if(this.listQuestions.questions.length  <=  this.pagination.pageSize && this.listQuestions.count > this.listQuestions.questions.length ) {
            this.getPageData();
          }
        }
        else
        {
          this.toastService.showWarning("Error.", "Question");

        }
        });
        
     }
   });
  }
}