import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { Route, Router } from '@angular/router';
import { SnackbarService } from '../snackbar-service';

@Injectable()
export class ApiErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private snackbarService: SnackbarService
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        console.log(error);
        
        this.snackbarService.getApiErrorSnackBar(
          error.error.status.toString(),
          error.error.title
        );
        throw error;
      })
    );
  }
}
