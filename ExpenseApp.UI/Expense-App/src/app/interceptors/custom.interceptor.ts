import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from '../services/login.service';

@Injectable()
export class CustomInterceptor implements HttpInterceptor {

  constructor(private loginService:LoginService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let modifiedReq = request;
    const token = this.loginService.getToken();
    console.log(token);
    if(token !== null){
      modifiedReq = modifiedReq.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
    }

    return next.handle(modifiedReq);
  }
}
