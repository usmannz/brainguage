import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pager } from '../entities/pager';
import { Settings } from '../helpers/settings';
import { SavePrepTestConfig } from '../entities/preptestconfig';
import { JAVASCRIPT } from './quiz.service';

@Injectable({
    providedIn: 'root'
})
export class PrepTestService {

    constructor(
        private _http: HttpClient
    ) {
    }
    getAllPrepTests(pagination: Pager): Observable<any> {
        return this._http.post<any>(`${Settings.apiBase}preptest/GetAllPrepTests`, pagination);
    }

    insertPrepTestConfig(config: SavePrepTestConfig): Observable<number> {
        return this._http.post<number>(`${Settings.apiBase}preptest/SavePrepTestConfig`, config)
    }

    savePrepTestResponse(ans: any): Observable<number> {
        return this._http.post<number>(`${Settings.apiBase}preptest/savePrepTestResponse`, ans)
    }

    getPrepTestById(PrepTestConfigId): Observable<any> {
        return this._http.get<number>(`${Settings.apiBase}preptest/getPrepTestById/${PrepTestConfigId}`)     
    }

    get(type: string): any {
        let data: any;
        switch (type) {
          case "javascript":
            return JAVASCRIPT;
          case "aspnet":
            return JAVASCRIPT;
          case "csharp":
            return JAVASCRIPT;
          case "designPatterns":
            return JAVASCRIPT;
        }
      }
    
      getAll() {
        return [
          { id: "javascript", name: "JavaScript" },
          { id: "aspnet", name: "Asp.Net" },
          { id: "csharp", name: "C Sharp" },
          { id: "designPatterns", name: "Design Patterns" }
        ];
      }

}
