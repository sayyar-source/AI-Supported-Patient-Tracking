import { Routes } from '@angular/router';
import { PatientListComponent } from './patient-list/patient-list.component';
import { PatientDetailComponent } from './patient-detail/patient-detail.component';
import { PatientCreateComponent } from './patient-create/patient-create.component';

export const routes: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { path: 'list', component: PatientListComponent },
  { path: ':id', component: PatientDetailComponent },
  { path: 'create', component: PatientCreateComponent },
];
