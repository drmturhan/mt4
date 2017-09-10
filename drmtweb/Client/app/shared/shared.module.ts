import { NgModule, ModuleWithProviders } from '@angular/core';
//import { ToastyModule } from 'ng2-toasty';
import { ToasterModule } from 'angular2-toaster';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { HttpModule, JsonpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { PageHeadingComponent } from './directives/page-heading.directive';
import { DynamicFormComponent } from './forms/dynamic-form.component';
import { DynamicFormControlComponent } from './forms/dynamic-form-control.component';
import { ErrorMessageComponent } from './forms/error-message.component';
import { ErrorSummaryComponent } from './forms/error-summary.component';
import { FormControlService } from './forms/form-control.service';


import { HeaderComponent } from './layout/header.component';
import { FooterComponent } from './layout/footer.component';

import { UppercasePipe } from './pipes/uppercase.pipe';

// Services
import { ContentService } from './services/content.service';
import { PagerService } from "./services/pager-service";
import { PaginationComponent } from "./components/sayfalama.component";
import { ModalComponent } from './components/modal-component/modal-component.component';
import { ModalTriggerDirective } from './directives/modal-trigger.directive';
import { LargeModalComponent } from './components/large-modal/large-modal.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        ToasterModule,
        TranslateModule,
        NgbModule.forRoot(),
        //ToastyModule.forRoot(),
        // No need to export as these modules don't expose any components/directive etc'
        HttpModule,
        JsonpModule
    ],
    declarations: [
        DynamicFormComponent,
        DynamicFormControlComponent,
        ErrorMessageComponent,
        ErrorSummaryComponent,
        FooterComponent,
        HeaderComponent,
        PageHeadingComponent,
        UppercasePipe,
        PaginationComponent,
        ModalComponent,
        ModalTriggerDirective,
        LargeModalComponent
    ],
    exports: [
        // Modules
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        NgbModule,
        TranslateModule,
        // Providers, Components, directive, pipes
        DynamicFormComponent,
        DynamicFormControlComponent,
        ErrorSummaryComponent,
        ErrorMessageComponent,
        FooterComponent,
        HeaderComponent,
        PageHeadingComponent,
        PaginationComponent,
        UppercasePipe,
        ToasterModule, ModalTriggerDirective,
        ModalComponent,
        LargeModalComponent
        //ToastyModule
    ]

})
export class SharedModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
                FormControlService,
                ContentService,
                PagerService

            ]
        };
    }
}
