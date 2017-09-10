import { NgModule} from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'angular2-toaster/toaster.css';
import 'jquery';
import 'bootstrap-loader';

import { BrowserModule } from '@angular/platform-browser';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { StoreModule } from '@ngrx/store';
import { HttpModule } from '@angular/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { ApiTranslationLoader } from './shared/services/api-translation-loader.service';
import { routing } from './app.routes';
import { AppService } from './app.service';
import { appReducer } from './app-store';
import { AppComponent } from './app.component';
import { JQ_TOKEN } from "./shared/services/jQueryService";
import { AuthService } from "./core/services/auth.service";
import { HomeModule } from "./home/home.module";
declare let jQuery: Object;

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        routing,
        StoreModule.provideStore(appReducer),
        StoreDevtoolsModule.instrumentOnlyWithExtension(),
        TranslateModule.forRoot({ loader: { provide: TranslateLoader, useClass: ApiTranslationLoader } }),
        // FormsModule,
        HttpModule,
        // Only module that app module loads
        CoreModule.forRoot(),
        SharedModule.forRoot(),
        HomeModule
    ],
    providers: [
        { provide: JQ_TOKEN, useValue: jQuery },
        AppService,
        AuthService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
