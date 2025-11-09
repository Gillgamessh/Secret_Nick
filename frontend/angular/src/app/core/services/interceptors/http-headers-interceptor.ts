import {
  HttpEvent,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';

export const httpHeadersInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const headers = req.headers
    .set('Accept', 'application/json')
    .set('Content-Type', 'application/json');
  const clonedRequest = req.clone({ headers });

  return next(clonedRequest);
};
