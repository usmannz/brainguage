import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pager } from '../entities/pager';
import { Settings } from '../helpers/settings';
import { Categories } from '../entities/categories';

@Injectable({
    providedIn: 'root'
})
export class CategoriesService {

    constructor(
        private _http: HttpClient
    ) {
    }
    getAllCategories(pagination: Pager): Observable<any> {
        return this._http.post<any>(`${Settings.apiBase}categories/getAllCategories`, pagination);
    }

    insertCategory(category: Categories): Observable<number> {
        return this._http.post<number>(`${Settings.apiBase}categories/saveCategory`, category)
    }

    deleteCategory(categoryId: number): Observable<number> {
        return this._http.delete<number>(`${Settings.apiBase}categories/${categoryId}`)     
    }

    getAllDropDownCategories(): Observable<any> {
        return this._http.get<any>(`${Settings.apiBase}categories/getAllDropDownCategories`);
    }

    getAllDropDownProducts(): Observable<any> {
        return this._http.get<any>(`${Settings.apiBase}categories/getAllProducts`);
    }


}
