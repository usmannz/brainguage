import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../entities/user';
import { Settings } from '../helpers/settings';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(
        private _http: HttpClient
    ) {
    }

    authenticate(user: User): Observable<any> {
        return this._http.post<User>(`${Settings.apiBase}users/auth`, user);
    }

   
}
