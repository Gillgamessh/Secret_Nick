import {
  Binding,
  Component,
  effect,
  inject,
  inputBinding,
  outputBinding,
  viewChild,
  ViewContainerRef,
} from '@angular/core';
import { ModalService } from '../../core/services/modal';

@Component({
  selector: 'app-modal-host',
  template: '<ng-container #vc />',
})
export class ModalHost {
  readonly #modalService = inject(ModalService);

  readonly vcr = viewChild.required('vc', { read: ViewContainerRef });

  readonly isModalOpen = this.#modalService.isModalOpen;

  readonly renderModalEffect = effect(() => {
    const entry = this.#modalService.currentModal();
    const bindings: Binding[] = [];
    this.vcr().clear();

    if (!entry) {
      return;
    }

    if (entry.inputs) {
      Object.entries(entry.inputs).forEach(([key, value]) => {
        bindings.push(inputBinding(key, () => value));
      });
    }

    if (entry.outputs) {
      Object.entries(entry.outputs).forEach(([key, handler]) => {
        bindings.push(outputBinding(key, handler));
      });
    }

    this.vcr().createComponent(entry.component, {
      bindings,
    });
  });
}
