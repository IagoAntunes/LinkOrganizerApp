import { HttpInterceptorFn } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

function isTokenExpired(token: string | null): boolean {
    if (!token) return true;
        try {
            const [, payload] = token.split('.');
            const decoded = JSON.parse(atob(payload));
        if (!decoded.exp) return true;
            return Date.now() / 1000 > decoded.exp;
        } catch {
        return true;
    }
}

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const token = localStorage.getItem('auth_token');
    let request = req;
    if (token) {
        request = req.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`,
        },
        });
    }

    return next(request).pipe(
        catchError((err) => {
            if (err.status === 401) {
                if (isTokenExpired(token)) {
                    localStorage.removeItem('auth_token');
                    window.location.href = '/login';
                }
            }
            return throwError(() => err);
        })
    );
};