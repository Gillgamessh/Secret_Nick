import { Component, computed, inject } from '@angular/core';

import { LoaderService } from '../../../core/services/loader';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.html',
  styleUrl: './loader.scss',
})
export class Loader {
  readonly #loaderService = inject(LoaderService);

  public readonly isVisible = computed(() => this.#loaderService.isVisible());
}
