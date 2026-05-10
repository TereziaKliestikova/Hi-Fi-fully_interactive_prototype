import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-edit-tags',
  templateUrl: './edit-tags.component.html',
  styleUrls: ['./edit-tags.component.scss'],
})
export class TagEditorComponent implements OnChanges {
  @Input() tagString: string | null | undefined = '';
  @Output() tagStringChange = new EventEmitter<string>();

  // Добавляем событие, которое будет эмититься при нажатии на крестик
  @Output() popupClose = new EventEmitter<void>();

  tags: string[] = [];

  editingIndex: number | null = null;
  editedTag: string = '';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tagString'].currentValue) {
      if (
        changes['tagString'] &&
        changes['tagString'].currentValue !== undefined
      ) {
        const newValue = changes['tagString'].currentValue as string;
        this.tags = newValue
          .split(';')
          .map(tag => tag.trim())
          .filter(tag => tag.length > 0);
      }
    }
  }

  // === Методы для редактирования тегов (без изменений) ===
  startEdit(index: number) {
    this.editingIndex = index;
    this.editedTag = this.tags[index];
  }

  saveEdit() {
    if (this.editingIndex !== null && this.editedTag.trim()) {
      this.tags[this.editingIndex] = this.editedTag.trim();
      this.emitTagStringChange();
    }
    this.cancelEdit();
  }

  cancelEdit() {
    this.editingIndex = null;
    this.editedTag = '';
  }

  deleteTag(index: number) {
    this.tags.splice(index, 1);
    this.emitTagStringChange();
  }

  addTag(tagInput: HTMLInputElement) {
    let value = tagInput.value.trim();
    if (value && !this.tags.includes(value)) {
      if (!value.startsWith('#')) {
        value = '#' + value;
      }
      this.tags.push(value);
      this.emitTagStringChange();
    }
    tagInput.value = '';
  }

  private emitTagStringChange() {
    this.tagStringChange.emit(this.tags.join(';'));
  }

  onBlur(): void {
    console.log('blure');
    this.popupClose.emit();
  }
  onClosePopup(): void {
    this.popupClose.emit();
  }
}
