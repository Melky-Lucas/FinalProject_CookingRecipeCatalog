import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ProblemDetails } from '../../models/problem-details.model';
import { KeyValuePipe } from '@angular/common';
import { errorMessages } from '../../utils/form.utils';
import { FieldErrorsPipe } from '../../Pipes/FieldErrors.pipe';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, KeyValuePipe, FieldErrorsPipe],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  ApiErrorMessages: Record<string, string[]> = {};
  error = '';
  loading = false;

  errorMessages = errorMessages;

  get hasApiErrors(): boolean {
    return this.ApiErrorMessages && Object.keys(this.ApiErrorMessages).length > 0;
  }


  readonly form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly auth: AuthService,
    private readonly router: Router
  ) {}

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.error = '';

    this.auth.login(this.form.getRawValue() as { email: string; password: string }).subscribe({
      next: () => {
        this.router.navigate(['/mis-recetas']);
      },
      error: (problem: ProblemDetails) => {

        if (problem.errors === null) {
          this.error = problem.detail || 'No se pudo iniciar sesión.';
        }
        else {
          this.ApiErrorMessages = problem.errors || { };

          if (Object.keys(this.ApiErrorMessages).length === 0) {
            this.ApiErrorMessages = { ...this.ApiErrorMessages, [problem.title || 'Ocurrió un error inesperado.']: [] };
          }
        }

        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
}
