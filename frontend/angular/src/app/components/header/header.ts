import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Path, AriaLabel } from '../../app.enum';
import { IMAGES_SPRITE_PATH } from '../../app.constants';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  readonly homeLink: Path = Path.Home;
  readonly ariaLabel: AriaLabel = AriaLabel.Logo;
  readonly logoHref: string = `${IMAGES_SPRITE_PATH}#logo`;
}
