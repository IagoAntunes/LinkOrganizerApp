import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';

export interface LoginResponse {
  token: string;
}

export interface RegisterRequest{
  name:string;
  lastName: string;
  email: string;
  password: string;
  roles: string[];
}


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7018'; 
  private readonly TOKEN_KEY = 'auth_token';

  private readonly http = inject(HttpClient);

  login(email:string, password:string) : Observable<string>{
        const body = { email, password };
    return this.http.post(`${this.apiUrl}/Login`, body,{ responseType: 'text' }).pipe(
      catchError(err => throwError(() => err))
    );
  }

  register(data: RegisterRequest) : Observable<any>{
    return this.http.post(`${this.apiUrl}/Register`, data).pipe(
      catchError(err => throwError(() => err))
    );
  }

  getUserData(){
    //
  }

  setToken(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
  }

}
