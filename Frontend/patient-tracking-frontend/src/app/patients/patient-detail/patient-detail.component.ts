import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PatientService } from '../../services/patient.service';

@Component({
  selector: 'app-patient-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './patient-detail.component.html',
  // styleUrls: ['./patient-detail.component.css'],
})
export class PatientDetailComponent implements OnInit {
  patient: any = null;
  prediction: any = null;

  constructor(private route: ActivatedRoute, private patientService: PatientService) {}

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.patientService.getPatient(id).subscribe((data) => {
      this.patient = data;
    });
    this.route.paramMap.subscribe((params) => {
      const id = String(params.get('id'));
      this.patientService.getPrediction(id).subscribe((data) => {
        this.prediction = data;
      });
    });
  }
}
