import {
  Component,
  computed,
  inject,
  input,
  OnInit,
  signal,
} from '@angular/core';
import {
  FormControl,
  ReactiveFormsModule,
  ValidationErrors,
} from '@angular/forms';

import { CaptionMessage } from '../../../app.enum';
import { FormValidation } from '../../../core/services/form-validation';
import { merge, tap } from 'rxjs';

@Component({
  selector: 'app-field-hint',
  imports: [ReactiveFormsModule],
  templateUrl: './field-hint.html',
  styleUrl: './field-hint.scss',
})
export class FieldHint implements OnInit {
  readonly control = input.required<FormControl>();
  readonly captionMessage = input<CaptionMessage>(CaptionMessage.EmptyMessage);

  readonly #errorsSig = signal<ValidationErrors | null>(null);
  readonly #formValidationService = inject(FormValidation);

  ngOnInit() {
    merge(this.control().valueChanges, this.control().statusChanges)
      .pipe(tap(() => this.#errorsSig.set(this.control().errors)))
      .subscribe();
  }

  readonly showError = computed(() => {
    this.#errorsSig();
    return (
      this.control().invalid && (this.control().touched || this.control().dirty)
    );
  });

  public readonly errorMessage = computed(() => {
    this.#errorsSig();
    return this.showError()
      ? this.#formValidationService.getErrorMessage(this.control())
      : null;
  });

  public readonly hintMessage = computed(() => {
    this.#errorsSig();
    return this.errorMessage() || this.captionMessage();
  });
}
