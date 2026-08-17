import { Pipe, type PipeTransform } from '@angular/core';
import { getErrors } from '../utils/form.utils';
import { FormGroup } from '@angular/forms';

@Pipe({
  name: 'fieldErrors',
  standalone: true,
})
export class FieldErrorsPipe implements PipeTransform {
  transform(fieldName: string, form: FormGroup): string[] {
    return getErrors(form, fieldName);
  }
}
