import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReturnJWT } from '../Interfaces/interfaces';

@Injectable()
export class JwtAdderInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const idToken = String(localStorage.getItem("token"));
    let stringTable: string;
    if(idToken == null)
    {

      stringTable = "id " + idToken;
    }

        if (idToken) {

            const cloned = request.clone({
                headers: request.headers.set("JwtToken", "testoweXD")
            });

            return next.handle(cloned);
        }
        else {
            return next.handle(request);
        }
  }
}
