import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { PatientService } from '../../services/patient.service';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './patient-list.component.html',
  //styleUrls: ['./patient-list.component.css'],
})
export class PatientListComponent implements OnInit {
  patients: any[] = [];

  constructor(private patientService: PatientService, private router: Router) {}

  ngOnInit(): void {
    this.patientService.getPatients().subscribe((data) => {
      this.patients = data;
    });
  }

  viewPatient(id: number): void {
    this.router.navigate(['/prediction', id]);
  }

  deletePatient(id: number): void {
    if (confirm('Are you sure you want to delete this patient?')) {
      this.patientService.deletePatient(id).subscribe(() => {
        this.patients = this.patients.filter((p) => p.id !== id);
      });
    }
  }

  addNewPatient(): void {
    this.router.navigate(['/patients/create']);
  }
}
