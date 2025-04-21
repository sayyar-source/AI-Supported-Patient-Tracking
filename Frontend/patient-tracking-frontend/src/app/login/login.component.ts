import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService, LoginRequest } from '../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  #fb = inject(FormBuilder);
  #authService = inject(AuthService);
  #router = inject(Router);

  loginForm = this.#fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(3)]],
  });

  errorMessage: string = '';

  onSubmit() {
    if (this.loginForm.valid) {
      this.#authService.login(this.loginForm.value as LoginRequest).subscribe({
        next: (response) => {
          localStorage.setItem('token', response.token);
          this.#router.navigate(['/patients']);
        },
        error: () => (this.errorMessage = 'Invalid credentials'),
      });
    }
  }
}
