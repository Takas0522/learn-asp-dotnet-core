import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, mergeMap, Observable } from 'rxjs';
import { MsalService } from './msal.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(
    private msalService: MsalService
  ) {}
intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.msalService.acquireToken()).pipe(
        mergeMap(m => {
            const nr = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${m}` )
            })
            return next.handle(nr);
        })
    )
  }
}