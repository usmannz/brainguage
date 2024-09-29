import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ContextService } from '../services/context.service';

// https://jasonwatmore.com/post/2018/05/23/angular-6-jwt-authentication-example-tutorial#app-module-ts

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(
        private _contextService: ContextService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this._contextService._token) {
            request = request.clone({
                setHeaders: { 
                    Authorization: `Bearer ${this._contextService._token}`
                }
            });
        }

        return next.handle(request);
    }
}