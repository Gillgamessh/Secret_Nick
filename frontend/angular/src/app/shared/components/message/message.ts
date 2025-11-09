import { Component, computed, input } from '@angular/core';
import { ICONS_SPRITE_PATH } from '../../../app.constants';
import { MessageSize, MessageType } from '../../../app.enum';

@Component({
  selector: 'app-message',
  imports: [],
  templateUrl: './message.html',
  styleUrl: './message.scss',
})
export class Message {
  readonly text = input.required<string>();
  readonly type = input<MessageType>(MessageType.Success);
  readonly size = input<MessageSize>(MessageSize.Toaster);

  readonly iconHref = computed(
    () => `${ICONS_SPRITE_PATH}#${this.type()}-message`
  );
}
