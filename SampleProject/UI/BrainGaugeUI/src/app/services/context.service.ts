import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

import { Router } from '@angular/router';
import { User } from '../entities/user';
import { Helper } from '../helpers/helper';


@Injectable({
    providedIn: 'root'
})
export class ContextService {
    _session: any = null;
    _currentUser: User;
    _token: string;
    _userRoles: any =null;
    get CurrentUserId(): number {
        if (this._session && this._session.uid) {
            return parseInt(this._session.uid);
        }
        return -1;
    }

    get CurrentTenantId(): number {
        if (this._session && this._session.tid) {
            return parseInt(this._session.tid);
        }
        return -1;
    }

    get CurrentUserName(): string {
        if (this._session && this._session.fullName) {
            return this._session.fullName;
        }
        return "";
    }
    get CurrentUserRole(): number {
        if(localStorage["role"])
        {
            this._userRoles = JSON.parse(localStorage["role"]);
            return this._userRoles.rolesId;
        }
        return -1;
    }
    constructor(
        private router: Router,

    ) { 
    }

    service_OnInit() {
        this.initUserSession();
    }

    // initUserSession /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    initUserSession() {
        if (localStorage["app"]) {
            var jwt = localStorage["app"];
            this._token = jwt;
            this._session = Helper.parseJwt(jwt);
        }

        if(localStorage["role"])
        {
            this._userRoles = JSON.parse(localStorage["role"]);
        }
    }

    // logout /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    logout() {
        localStorage.clear();
        sessionStorage.clear();
        this._session = null;
        this._currentUser = null;
        this._token = null;

        this.router.navigate(['/login']);
    }
    
}

interface Dictionary<T> {
    [Key: string]: T;
}
