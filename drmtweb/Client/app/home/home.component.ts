import { Component, OnInit } from '@angular/core';
import { routerTransition, hostStyle } from '../router.animations';
import { Observable } from "rxjs/Observable";
import { Store } from "@ngrx/store";
import { AuthState } from "../core/auth-store/auth.store";
import { AppState } from "../app-store";

@Component({
    selector: 'appc-home',
    styleUrls: ['./home.component.scss'],
    templateUrl: './home.component.html',
    animations: [routerTransition()],
    // tslint:disable-next-line:use-host-property-decorator
    host: hostStyle()
})
export class HomeComponent implements OnInit {
    public authState$: Observable<AuthState>;
    constructor(
        public store: Store<AppState>,

    ) { }
    public ngOnInit(): void {
        this.authState$ = this.store.select(state => state.auth);
    }
}
