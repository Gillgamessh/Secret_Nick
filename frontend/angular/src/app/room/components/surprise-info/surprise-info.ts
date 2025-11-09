import { Component, input } from '@angular/core';

@Component({
  selector: 'app-surprise-info',
  templateUrl: './surprise-info.html',
  styleUrl: './surprise-info.scss',
})
export class SurpriseInfo {
  readonly text = input.required<string>();
}
