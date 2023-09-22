import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { AuthUser } from '../_models/user';

@Injectable({
    providedIn: 'root'
})
export class AccountService {

    baseUrl = 'https://localhost:5001/api/';

    private currentAuthUserSource = new BehaviorSubject<AuthUser | null>(null);
    currentAuthUser$ = this.currentAuthUserSource.asObservable();

    constructor(private http: HttpClient) { }

    login(model: any): Observable<any> {
        return this.http.post<AuthUser>(this.baseUrl + 'auth/login', model).pipe(
            map((response: AuthUser) => {
                const user = response;                
                if (user) {
                    localStorage.setItem('user', JSON.stringify(user));
                    this.currentAuthUserSource.next(user);
                }
                return user;
            })
        );
    }

    register(model: any) {
        return this.http.post<AuthUser>(this.baseUrl + 'auth/register', model).pipe(            
            map((response: AuthUser) => {
                const user = response;                
                if (user) {
                    localStorage.setItem('user', JSON.stringify(user));
                    this.currentAuthUserSource.next(user);
                }
                return user;
            }
            )
        )
    }

    setCurrentUser(user: AuthUser) {
        this.currentAuthUserSource.next(user);
    }

    logout() {
        localStorage.removeItem('user');
        this.currentAuthUserSource.next(null);
    }
}
