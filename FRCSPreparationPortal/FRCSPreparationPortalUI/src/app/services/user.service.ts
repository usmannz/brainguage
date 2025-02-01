import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SignUp, User } from '../entities/user';
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
    getAllDropDownUsers(): Observable<any> {
        return this._http.get<any>(`${Settings.apiBase}users/getAllDropDownUsers`);
    }
    signUpUser(signUp: SignUp): Observable<number> {
        return this._http.post<any>(`${Settings.apiBase}users/signUpUser`,signUp);
    }

    createStripeSession(userId: number): Observable<any> {
        return this._http.post<any>(`${Settings.apiBase}users/create-session`,userId);
    }

}
