import { Component, OnInit } from '@angular/core';
//import { ViewEncapsulation} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { AppState } from './app-store';
import { AuthState } from './core/auth-store/auth.store';
import { ToasterConfig } from "angular2-toaster";
import { AuthTokenService } from "./core/auth-token/auth-token.service";
import { Router, Event, NavigationStart, NavigationEnd, NavigationError, NavigationCancel } from "@angular/router";
import { DataService } from "./core/services/data.service";

@Component({
   
    selector: 'appc-root',
    templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        animation: 'fade',
        timeout: 7000,
        limit: 8,
        tapToDismiss: true,
        newestOnTop: false,
        mouseoverTimerStop: true,
        showCloseButton: true

    });

    private authState$: Observable<AuthState>;
    private yukleniyor: boolean = false;
    private veritabaniIslemiVar: boolean = false;
    constructor(
        public translate: TranslateService,
        public titleService: Title,
        private tokens: AuthTokenService,
        private store: Store<AppState>,
        private router: Router,
        private apiGateway: DataService) {
        // this language will be used as a fallback when a translation isn't found in the current language
        translate.setDefaultLang('tr');

        // the lang to use, if the lang isn't available, it will use the current loader to get them
        translate.use('tr');
        this.router.events.subscribe((routerEvent: Event) => {
            this.olayKontroluYap(routerEvent)
        });
    }
    olayKontroluYap(olay: Event) {
        if (olay instanceof NavigationStart) { this.yukleniyor = true; }
        if (olay instanceof NavigationEnd || olay instanceof NavigationCancel || olay instanceof NavigationError) { this.yukleniyor = false; }
    }
    public ngOnInit() {
        this.translate.onLangChange.subscribe((lan: string) => {
            this.translate.get('TITLE')
                .subscribe(title => this.setTitle(title));

        });
        this.apiGateway.pendingCommands$.subscribe(x => {
            this.veritabaniIslemiVar = x > 0;
        });

        this.authState$ = this.store.select(state => state.auth);
        //this.authState$.subscribe((durum: AuthState) => {
        //    if (!durum.loggedIn) {
        //        this.router.navigate(['/sistem/giris'], { queryParams: { returnUrl: this.router.url } });
        //    }
        //});
        // This starts up the token refresh preocess for the app
        this.tokens.startupTokenRefresh()
            .subscribe(
            () => {
                console.info('Başlangıç başarılı')
            },
            error => console.warn(error)
            );
    }
    public setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);
    }
}
