import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Settings } from '../helpers/settings';

@Injectable({
    providedIn: 'root'
})
export class HomeService {
    private _endPoint = `${Settings.apiBase}`;

    constructor(private _http: HttpClient) { }

    clearCache(): Observable<string> {
        return this._http.get<string>(`${this._endPoint}cache/burst`)
    }

    
}
