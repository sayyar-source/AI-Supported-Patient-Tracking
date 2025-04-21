import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PatientService } from '../../services/patient.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './patient-create.component.html',
  styles: [
    `
      :host {
        display: block;
        font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
      }

      .container {
        max-width: 500px;
        margin: 2rem auto;
        padding: 0 1rem;
      }

      .card {
        background: #ffffff;
        border-radius: 12px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1), 0 1px 3px rgba(0, 0, 0, 0.08);
        padding: 2rem;
        transition: transform 0.2s ease-in-out;
      }

      .card:hover {
        transform: translateY(-2px);
      }

      h2 {
        font-size: 1.5rem;
        font-weight: 600;
        color: #1a202c;
        margin-bottom: 1.5rem;
        text-align: center;
      }

      .form-group {
        margin-bottom: 1.5rem;
      }

      label {
        display: block;
        font-size: 0.875rem;
        font-weight: 500;
        color: #4a5568;
        margin-bottom: 0.5rem;
      }

      .form-control {
        width: 100%;
        padding: 0.75rem 1rem;
        font-size: 1rem;
        line-height: 1.5;
        color: #2d3748;
        background-color: #f7fafc;
        border: 1px solid #e2e8f0;
        border-radius: 8px;
        transition: border-color 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
      }

      .form-control:focus {
        outline: none;
        border-color: #3182ce;
        box-shadow: 0 0 0 3px rgba(49, 130, 206, 0.1);
      }

      .form-control::placeholder {
        color: #a0aec0;
      }

      .btn {
        display: inline-block;
        font-weight: 500;
        text-align: center;
        white-space: nowrap;
        vertical-align: middle;
        user-select: none;
        padding: 0.75rem 1.5rem;
        font-size: 1rem;
        line-height: 1.5;
        border-radius: 8px;
        transition: all 0.2s ease-in-out;
        cursor: pointer;
        margin-right: 0.5rem;
      }

      .btn-primary {
        color: #fff;
        background-color: #3182ce;
        border: 1px solid #2b6cb0;
      }

      .btn-primary:hover:not(:disabled) {
        background-color: #2b6cb0;
        border-color: #2a5aa0;
      }

      .btn-primary:disabled {
        opacity: 0.6;
        cursor: not-allowed;
      }

      .btn-secondary {
        color: #fff;
        background-color: #718096;
        border: 1px solid #5a7088;
      }

      .btn-secondary:hover {
        background-color: #5a7088;
        border-color: #4a5568;
      }

      @media (max-width: 640px) {
        .container {
          margin: 1rem auto;
          padding: 0 0.5rem;
        }

        .card {
          padding: 1.5rem;
        }

        .btn {
          width: 100%;
          margin-bottom: 0.5rem;
          margin-right: 0;
        }
      }
    `,
  ],
})
export class PatientCreateComponent {
  patientForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private patientService: PatientService,
    private router: Router
  ) {
    this.patientForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      birthdate: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.patientForm.valid) {
      this.patientService.createPatient(this.patientForm.value).subscribe(() => {
        this.router.navigate(['/patients']);
      });
    }
  }
}
