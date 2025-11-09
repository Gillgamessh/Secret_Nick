import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { Input } from '../input/input';
import { InputPlaceholder, ItemPosition } from '../../../app.enum';
import { BaseLabel } from '../../../app.enum';
import { CharCounter } from '../../../core/directives/char-counter';
import { FieldHint } from '../field-hint/field-hint';

@Component({
  selector: 'app-gift-idea',
  imports: [Input, CharCounter, ReactiveFormsModule, FieldHint],
  templateUrl: './gift-idea.html',
  styleUrl: './gift-idea.scss',
})
export class GiftIdea {
  readonly wishControl = input.required<FormControl>();
  readonly linkControl = input.required<FormControl>();

  readonly wishMaxLength = input<number | null>(null);
  readonly isWishInputRequired = input<boolean>(false);

  public readonly wishPlaceholder = InputPlaceholder.WishPlaceholder;
  public readonly linkPlaceholder = InputPlaceholder.LinkPlaceholder;
  public readonly wishLabel = BaseLabel.GiftIdeaWish;
  public readonly linkLabel = BaseLabel.GiftIdeaLink;
  public readonly y = ItemPosition.Center;
}
