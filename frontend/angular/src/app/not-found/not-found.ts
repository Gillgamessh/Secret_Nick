import { Component } from '@angular/core';
import { IconName, PageTitle, Path } from '../app.enum';
import { RouterLink } from '@angular/router';
import { Button } from '../shared/components/button/button';
import { IMAGES_SPRITE_PATH } from '../app.constants';

@Component({
  selector: 'app-not-found',
  imports: [Button, RouterLink],
  templateUrl: './not-found.html',
  styleUrl: './not-found.scss',
})
export class NotFound {
  public readonly homeLink: Path = Path.Home;
  public readonly homeButtonIconName = IconName.House;
  public readonly imageHref: string = `${IMAGES_SPRITE_PATH}#not-found`;
  public readonly title: PageTitle = PageTitle.NotFound;
}
