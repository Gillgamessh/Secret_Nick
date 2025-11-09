import { Injectable } from '@angular/core';
import lottie, { AnimationItem } from 'lottie-web';

import type { LottieConfig } from '../../app.models';

@Injectable({ providedIn: 'root' })
export class LottieAnimationService {
  #currentAnimation?: AnimationItem;

  public play(config: LottieConfig): void {
    this.destroy();

    this.#currentAnimation = lottie.loadAnimation({
      container: config.container,
      renderer: 'svg',
      loop: config.loop ?? false,
      autoplay: config.autoplay ?? true,
      path: config.path,
    });

    if (config.speed) {
      this.#currentAnimation.setSpeed(config.speed);
    }

    if (config.onComplete) {
      this.#currentAnimation.addEventListener('complete', () => {
        config.onComplete?.();
        this.destroy();
      });
    }
  }

  public destroy(): void {
    if (this.#currentAnimation) {
      this.#currentAnimation.destroy();
      this.#currentAnimation = undefined;
    }
  }
}
