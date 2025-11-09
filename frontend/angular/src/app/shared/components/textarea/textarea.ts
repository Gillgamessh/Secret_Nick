import { Component, input, viewChild, ElementRef } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { Label } from '../label/label';
import { TextareaPlaceholder } from '../../../app.enum';
import type { InputLabel } from '../../../app.models';

@Component({
  selector: 'app-textarea',
  imports: [Label, ReactiveFormsModule],
  templateUrl: './textarea.html',
  styleUrl: './textarea.scss',
})
export class Textarea {
  label = input.required<InputLabel>();
  control = input.required<FormControl>();
  placeholder = input.required<TextareaPlaceholder>();

  isRequiredField = input<boolean>(false);
  maxLength = input<number | null>(null);
  minRows = input<number>(2);

  private textareaRef =
    viewChild.required<ElementRef<HTMLTextAreaElement>>('textarea');

  onInput(): void {
    this.resizeToContent();
  }

  private resizeToContent(): void {
    const el = this.textareaRef().nativeElement;
    el.style.height = 'auto';
    el.style.height = `${el.scrollHeight}px`;
  }
}
