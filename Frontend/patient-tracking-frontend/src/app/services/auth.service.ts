import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export type LoginRequest = {
  email: string;
  password: string;
}

type LoginResponse = {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  #http = inject(HttpClient)


  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.#http.post<LoginResponse>(`${environment.authApiUrl}/auth/login`, credentials);
  }
}