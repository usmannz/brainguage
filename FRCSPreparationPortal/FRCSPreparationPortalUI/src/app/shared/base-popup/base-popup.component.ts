import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base/base.component';
import { ContextService } from '../../services/context.service';
import { Router } from '@angular/router';
import { Roles, SideMenu } from '../enums';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-base-popup',
    template: '',
})
export class BasePopupComponent  extends BaseComponent implements OnInit {

    constructor(
        protected _contextService: ContextService,
        protected _dialogRef: MatDialogRef<any>,
        protected _router: Router,
    ) { 
        super(_contextService,_router);
    }

    ngOnInit() {
    }

    // closeDialog ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    closeDialog() {
        this._dialogRef.close(false);
    }

    checkUserRole(sideMenu: SideMenu) {
        switch (sideMenu) {
          case SideMenu.Admin: {
            var roleCheck = this.contextService._userRoles.find(item => item.roleId == Roles.Admin);
            if (roleCheck) {
              return true;
            }
            else {
              return false;
            }
          }
          case SideMenu.User: {
            var roleCheck = this.contextService._userRoles.find(item => item.roleId == Roles.User);
            if (roleCheck) {
              return true;
            }
            else {
              return false;
            }
          }
          default: {
            break;
          }
        }
      }

}
