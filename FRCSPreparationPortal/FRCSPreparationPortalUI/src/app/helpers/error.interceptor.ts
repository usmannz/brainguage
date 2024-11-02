import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ContextService } from '../services/context.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    
    constructor(
        private _contextService: ContextService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this._contextService.logout();
            }

            //const error = err.message || (err.error ? err.error.message : err.statusText);
            return throwError(err);
        }))
    }
}