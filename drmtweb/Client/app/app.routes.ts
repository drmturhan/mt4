import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { HomeComponent } from "./home/home.component";
const routes: Routes = [
    { path: '', redirectTo: 'anasayfa', pathMatch: 'full' },
    // Lazy async modules
    {
        path: 'anasayfa', component: HomeComponent
    },
    {
        path: 'sistem',   loadChildren: './+login/login.module#LoginModule'
    },
    {
        path: 'hesap', loadChildren: './+register/register.module#RegisterModule'
    },
    {
        path: 'profile', loadChildren: './+profile/profile.module#ProfileModule'
    },
    {
        path: 'admin', loadChildren: './+admin/admin.module#AdminModule'
    }

];

export const routing = RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules });
