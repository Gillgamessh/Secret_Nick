import { Pipe, PipeTransform } from '@angular/core';
import { BudgetControl } from '../../app.models';

@Pipe({
  name: 'budget',
  standalone: true,
})
export class BudgetPipe implements PipeTransform {
  transform(value: BudgetControl | undefined, currency = 'UAH'): string {
    if (!value) {
      return 'Unlimited';
    }
    return `${value} ${currency}`;
  }
}
