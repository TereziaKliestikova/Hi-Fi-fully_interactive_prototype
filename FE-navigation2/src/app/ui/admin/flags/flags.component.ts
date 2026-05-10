import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FlagTypeDto } from 'src/app/api/models';

@Component({
  selector: 'app-flags',
  templateUrl: './flags.component.html',
  styleUrls: ['./flags.component.scss'],
})
export class FlagsComponent {
  @Output() openEditFlagsModal = new EventEmitter<void>();
  @Input() flags: FlagTypeDto[] = [
    { name: 'Dokončiť', color: '#F75757' },
    { name: 'Zaujímavé', color: '#F7F157' },
    { name: 'Cvičenie pondelok', color: '#57F784' },
  ];
  @Output() openAddFlagModal = new EventEmitter<void>();

  onEditClick() {
    this.openEditFlagsModal.emit();
  }

  onAddClick() {
    this.openAddFlagModal.emit();
  }

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  onFlagClick(flag: FlagTypeDto) {}
}
