import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AuthResponse, AuthUser, LoginRequest, RegisterRequest } from '../models/auth.models';

const TOKEN_KEY = 'recipe_catalog_token';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly currentUser = signal<AuthUser | null>(this.loadUserFromStorage());

  readonly user = this.currentUser.asReadonly();

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}

  login(credentials: LoginRequest) {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/Auth/login`, credentials).pipe(
      tap(response => this.persistSession(response))
    );
  }

  register(data: RegisterRequest) {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/Auth/register`, data).pipe(
      tap(response => this.persistSession(response))
    );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    this.currentUser.set(null);
    this.router.navigate(['/']);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token && !this.isTokenExpired(token);
  }

  getUserId(): number | null {
    return this.currentUser()?.userId ?? null;
  }

  private persistSession(response: AuthResponse): void {
    localStorage.setItem(TOKEN_KEY, response.token);
    this.currentUser.set(this.decodeUser(response.token, response.username, response.role));
  }

  private loadUserFromStorage(): AuthUser | null {
    const token = localStorage.getItem(TOKEN_KEY);
    if (!token || this.isTokenExpired(token)) {
      localStorage.removeItem(TOKEN_KEY);
      return null;
    }

    const payload = this.parseJwt(token);
    return {
      userId: Number(payload['sub']),
      username: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] ?? payload['name'] ?? '',
      email: payload['email'] ?? payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] ?? '',
      role: payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ?? payload['role'] ?? ''
    };
  }

  private decodeUser(token: string, username: string, role: string): AuthUser {
    const payload = this.parseJwt(token);
    return {
      userId: Number(payload['sub']),
      username,
      email: payload['email'] ?? '',
      role
    };
  }

  private parseJwt(token: string): Record<string, string> {
    const base64 = token.split('.')[1];
    return JSON.parse(atob(base64));
  }

  private isTokenExpired(token: string): boolean {
    const payload = this.parseJwt(token);
    if (!payload['exp']) {
      return false;
    }
    return Date.now() >= Number(payload['exp']) * 1000;
  }
}
