import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ProblemDetails } from '../../models/problem-details.model';
import { KeyValuePipe } from '@angular/common';
import { errorMessages } from '../../utils/form.utils';
import { FieldErrorsPipe } from '../../Pipes/FieldErrors.pipe';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, KeyValuePipe, FieldErrorsPipe],
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  ApiErrorMessages: Record<string, string[]> = {};
  error = '';
  loading = false;

  readonly form = this.fb.group({
    username: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly auth: AuthService,
    private readonly router: Router
  ) {}

  get hasApiErrors(): boolean {
    return this.ApiErrorMessages && Object.keys(this.ApiErrorMessages).length > 0;
  }

  errorMessages = errorMessages;

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.error = '';

    this.auth.register(this.form.getRawValue() as { username: string; email: string; password: string }).subscribe({
      next: () => {
        this.router.navigate(['/mis-recetas']);
      },
      error: (problem: ProblemDetails) => {
        if (problem.errors === null) {
          this.error = problem.detail || 'No se pudo crear la cuenta.';
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
