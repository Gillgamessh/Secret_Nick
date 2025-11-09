import { Component } from '@angular/core';
import { PRIVACY_POLICY_PATH, PRIVACY_NOTICE_PATH } from '../../app.constants';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.html',
  styleUrl: './footer.scss',
})
export class Footer {
  readonly year = new Date().getFullYear();
  readonly policyUrl = PRIVACY_POLICY_PATH;
  readonly noticeUrl = PRIVACY_NOTICE_PATH;
}
