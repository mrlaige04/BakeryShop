import { HttpInterceptorFn } from '@angular/common/http';
import {inject} from "@angular/core";
import {AuthService} from "./auth.service";

export const passTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService)
  const token = auth.authToken;

  if (auth.isAuthenticated() && token()) {
    return next(req.clone({
      setHeaders: {
        'Authorization': `Bearer ${token()?.accessToken}`
      }
    }))
  }

  return next(req);
};
