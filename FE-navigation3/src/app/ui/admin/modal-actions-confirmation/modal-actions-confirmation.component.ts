import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ConfirmModalConfig } from './modal-actions-confirmation.config'; // относительный путь

@Component({
  selector: 'app-modal-actions-confirmation',
  templateUrl: './modal-actions-confirmation.component.html',
  styleUrls: ['./modal-actions-confirmation.component.scss'],
})
export class ConfirmModalComponent {
  @Input() config!: ConfirmModalConfig | null;
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();
  @Output() create = new EventEmitter<string>();
  inputData: string = '';

  onConfirm() {
    if (this.config?.paragraphs) this.confirm.emit();
    else this.create.emit(this.inputData);
  }

  onCancel() {
    this.cancel.emit();
  }
}
