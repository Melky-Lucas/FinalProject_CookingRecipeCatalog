import { FormGroup } from "@angular/forms";

export function isFieldInvalid(form: FormGroup, fieldName: string): boolean {
    const control = form.get(fieldName);
    return !!control && control.invalid && (control.dirty || control.touched);
}

export function getErrors(form: FormGroup, fieldName: string) {
    const control = form.get(fieldName);
    return control?.errors ? Object.keys(control.errors) : [];
}

export const errorMessages: Record<string, string> = {
    "required": "es obligatorio.",
    "minlength": "no cumple con la longitud mínima requerida.",
    "maxlength": "excede la longitud máxima permitida.",
    "min": "debe ser un número positivo.",
    "email": "no es un correo electrónico válido.",
};