import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContextService } from '../../services/context.service';
import { BasePopupComponent } from '../base-popup/base-popup.component';
import { PageAccessType } from '../enums';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
})
export class ConfirmationDialogComponent extends BasePopupComponent implements OnInit {
  // title: string;
  // message: string;
  constructor(
    protected _dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public _data: any,
    // public _dialog: MatDialog,
    protected _contextService: ContextService,
    protected _router: Router,

  ) {
    super(_contextService, _dialogRef,_router);
    this._pageAccessType = PageAccessType.PRIVATE;
   }
  

  ngOnInit() {
  }

   deleteConfirmationTextWrap(text: string): string {
    if (text.length > 5) {
      return text.substring(0, 10) + '...';
    }
    return text;
  }

}
