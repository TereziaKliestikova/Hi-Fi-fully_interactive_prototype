import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-toggle-button',
  templateUrl: './toggle.component.html',
  styleUrls: ['./toggle.component.scss'],
})
export class ToggleButtonComponent {
  @Input() checked = false;
  @Input() disabled = false;

  @Output() checkedChange = new EventEmitter<boolean>();

  onToggle(): void {
    if (this.disabled) {
      return;
    }

    this.checked = !this.checked;
    this.checkedChange.emit(this.checked);
  }

  get label(): string {
    return this.checked ? 'Zobraziť relevantné' : 'Skryť relevantné';
  }
}
