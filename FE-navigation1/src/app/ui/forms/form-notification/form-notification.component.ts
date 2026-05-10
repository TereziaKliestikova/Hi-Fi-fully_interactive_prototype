import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-form-notification',
  templateUrl: './form-notification.component.html',
  styleUrls: ['./form-notification.component.scss'],
})
export class FormNotificationComponent {
  message: Observable<string> | null = null;
  error: string | null = null;

  // TODO: Not implemented neatly
  //  remove error variable and use message for everything.
  //  this is because of showing errors which are received strings not observables

  showMessage(str: Observable<string>) {
    this.error = null;
    this.message = str;
  }

  showError(str: string) {
    this.message = null;
    this.error = str;
  }

  hide() {
    this.message = null;
    this.error = null;
  }
}
