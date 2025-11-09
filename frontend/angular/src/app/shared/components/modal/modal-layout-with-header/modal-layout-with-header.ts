import { Component } from '@angular/core';

import { Button } from '../../button/button';
import { IconButton } from '../../icon-button/icon-button';
import { FocusTrap } from '../../../../core/directives/focus-trap';
import { ParentModalLayout } from '../../../../core/directives/parent-modal-layout';
import { fadeIn } from '../../../../utils/animations';
import { FADE_IN_ANIMATION_DURATION_MS } from '../../../../app.constants';

@Component({
  selector: 'app-modal-layout-with-header',
  imports: [Button, IconButton, FocusTrap],
  templateUrl: './modal-layout-with-header.html',
  styleUrl: './modal-layout-with-header.scss',
  animations: [fadeIn(FADE_IN_ANIMATION_DURATION_MS)],
})
export class ModalLayoutWithHeader extends ParentModalLayout {}
