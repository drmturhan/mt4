import { ErrorHandler, Inject, NgZone, Injectable, Injector } from '@angular/core';
import { ToasterService } from "angular2-toaster";
import { Router } from "@angular/router";

@Injectable()
export class MTAppErrorHandler implements ErrorHandler {


    constructor(private ngZone: NgZone, @Inject(ToasterService) private toaster: ToasterService, private injector: Injector)
    { }
    handleError(error: any): void {
        this.ngZone.run(() => {
            this.toaster.pop("error", "Hata", "Beklenmedik bir hata oluştu. Bu nedenle ana sayfaya dönüldü. Hata mesajı almaya devam ediyorsa lütfen sistem yöneticisine bilgi verin!");
            console.log(error);
        })
        const router: Router = this.injector.get(Router);
        router.navigateByUrl(router.url);
    }
}
