import { Component, HostBinding, input } from '@angular/core';
import { Link } from '../../../../../shared/components/link/link';

@Component({
  selector: 'li[app-item-card]',
  imports: [Link],
  templateUrl: './item-card.html',
  styleUrl: './item-card.scss',
})
export class ItemCard {
  @HostBinding('tabindex') tab = 0;

  readonly text = input<string>('');
  readonly link = input<string>('');
}
