import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './+register/register.component';
import { RegisterConfirmationComponent } from './+confirmation/register-confirmation.component';

const routes: Routes = [
    { path: '', redirectTo: 'uyelik', pathMatch: 'full' },
    { path: 'uyelik', component: RegisterComponent },
    { path: 'uyeliktamam', component: RegisterConfirmationComponent }
];

export const routing = RouterModule.forChild(routes);
