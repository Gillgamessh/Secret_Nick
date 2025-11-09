import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  readonly #isVisible = signal<boolean>(false);
  readonly #requestCount = signal<number>(0);

  public readonly isVisible = this.#isVisible.asReadonly();

  public show(): void {
    this.#incrementRequestCount();

    if (this.#requestCount() === 1) {
      this.#isVisible.set(true);
    }
  }

  public hide(): void {
    this.#decrementRequestCount();

    if (this.#requestCount() === 0) {
      this.#isVisible.set(false);
    }
  }

  #incrementRequestCount(): void {
    this.#requestCount.update((prev) => prev + 1);
  }

  #decrementRequestCount(): void {
    this.#requestCount.update((prev) => prev - 1);
  }
}
