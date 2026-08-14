import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { ProblemDetails } from '../models/problem-details.model';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const problemDetails: ProblemDetails = error.error;
      
      if (problemDetails?.title) {
        console.error('Error estructurado de la API:', problemDetails.detail);
      }
      
      return throwError(() => problemDetails || error);
    })
  );
};