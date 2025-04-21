import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { authGuard } from './services/auth.guard';
import { Error404Component } from './error404/error404.component';
import { PatientCreateComponent } from './patients/patient-create/patient-create.component';
import { PatientDetailComponent } from './patients/patient-detail/patient-detail.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'patients/create', component: PatientCreateComponent },
  { path: 'patients/create', component: PatientCreateComponent },
  { path: 'prediction/:id', component: PatientDetailComponent },
  {
    path: 'patients',
    loadChildren: () => import('./patients/patients.routes').then((m) => m.routes),
    canActivate: [authGuard],
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', component: Error404Component }, // Fallback for invalid routes
];
