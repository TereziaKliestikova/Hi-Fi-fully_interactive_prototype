import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FlagTypeDto } from 'src/app/api/models';
@Component({
  selector: 'app-delete-confirm',
  templateUrl: './edit-flags-delete-confirmation.component.html',
  styleUrls: ['./edit-flags-delete-confirmation.component.scss'],
})
export class DeleteFlagConfirmComponent {
  @Input() flag: FlagTypeDto | null = null; // Название удаляемого тега
  @Output() closeComponent = new EventEmitter<void>();
  @Output() delete = new EventEmitter<{ name: string; color: string }>();

  closeModal() {
    this.closeComponent.emit();
  }

  confirmDelete() {
    if (this.flag) this.delete.emit(this.flag);
  }
}
