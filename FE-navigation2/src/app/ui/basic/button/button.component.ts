import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent {
  @Input() type: 'button' | 'submit' | 'reset' = 'button';
  @Input() state: 'loading' | 'disabled' | 'enabled' = 'enabled';
  @Input() color: 'primary' | 'accent' | 'warn' = 'primary';
}
