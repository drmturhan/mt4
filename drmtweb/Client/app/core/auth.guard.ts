import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Store } from "@ngrx/store";
import { AppState } from "../app-store";
import { AuthState } from "./auth-store/auth.store";

@Injectable()
export class AuthGuard implements CanActivate {

    public authState$: Observable<AuthState>;
    private loggedIn: boolean;
    private authReady: boolean;

    constructor(public store: Store<AppState>, private router: Router) {
        this.authState$ = this.store.select(state => state.auth);
        this.authState$.subscribe((data) => {
            this.loggedIn = data.loggedIn;
            this.authReady = data.authReady;
        });
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        if (this.loggedIn === true && this.authReady === true) return true;;
        this.router.navigate(['/sistem/giris'], { queryParams: { returnUrl: state.url } });
        return false;
        // not logged in so redirect to login page with the return url and return false
    }

}
