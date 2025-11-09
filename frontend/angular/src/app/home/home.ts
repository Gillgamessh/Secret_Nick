import { Component } from '@angular/core';
import { Button } from '../shared/components/button/button';
import { Path } from '../app.enum';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [Button, RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  readonly createRoomHref: Path = Path.CreateRoom;
}
