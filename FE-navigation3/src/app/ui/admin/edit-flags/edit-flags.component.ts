/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  Component,
  EventEmitter,
  SimpleChanges,
  Output,
  Input,
  OnChanges,
} from '@angular/core';
import { FlagTypeDto } from 'src/app/api/models';
@Component({
  selector: 'app-edit-flags',
  templateUrl: './edit-flags.component.html',
  styleUrls: ['./edit-flags.component.scss'],
})
export class EditFlagsComponent implements OnChanges {
  @Output() closeComponent = new EventEmitter<void>();
  @Output() edit = new EventEmitter<{
    name: string;
    color: string;
    id: number;
  }>();
  @Output() delete = new EventEmitter<{ name: string; color: string }>();
  @Input() labelName: string = ''; // Название удаляемого тега
  @Input() flags: FlagTypeDto[] = [
    { name: 'Dokončiť', color: '#ff4d4d' },
    { name: 'Zaujímavé', color: '#ffff66' },
    { name: 'Cvičenie pondelok', color: '#00cc66' },
    { name: 'Lorem', color: '#ffa64d' },
    { name: 'Ipsum', color: '#ff6666' },
  ];

  alreadyUsedColors: string[] = [];

  colors: string[] = [
    '#ff4d4d',
    '#ffa64d',
    '#ffff66',
    '#a1d36e',
    '#66cc66',
    '#33cccc',
    '#3399ff',
    '#6666ff',
    '#9b6cff',
    '#cc66ff',
    '#ff66b2',
    '#ff9999',
  ];

  enableColors: string[] = [
    'rgba(255, 77, 77, 0.10)', // #ff4d4d
    'rgba(255, 166, 77, 0.10)', // #ffa64d
    'rgba(255, 255, 102, 0.10)', // #ffff66
    'rgba(161, 211, 110, 0.10)', // #a1d36e
    'rgba(102, 204, 102, 0.10)', // #66cc66
    'rgba(51, 204, 204, 0.10)', // #33cccc
    'rgba(51, 153, 255, 0.10)', // #3399ff
    'rgba(102, 102, 255, 0.10)', // #6666ff
    'rgba(155, 108, 255, 0.10)', // #9b6cff
    'rgba(204, 102, 255, 0.10)', // #cc66ff
    'rgba(255, 102, 178, 0.10)', // #ff66b2
    'rgba(255, 153, 153, 0.10)', // #ff9999
  ];
  selectedFlag: FlagTypeDto = this.flags[0];
  selectedColor: string = '';
  isColorPickerVisible: boolean = false;
  activeFlagId = -1;
  deleteConfirmationModalShow: boolean = false;

  ngOnChanges(changes: SimpleChanges) {
    if (changes['flags'] && changes['flags'].currentValue) {
      this.alreadyUsedColors = this.flags.map(fg => fg.color);
    }
  }
  closeModal() {
    this.closeComponent.emit();
  }

  setColor(color: string) {
    let actualColor = color;
    let actualSize = '20px';
    let cursor = 'pointer';
    if (this.alreadyUsedColors.includes(color)) {
      actualColor = this.enableColors[this.colors.indexOf(color)];
      actualSize = '17px';
      cursor = 'default';
    }

    return {
      'background-color': actualColor,
      width: actualSize,
      height: actualSize,
      cursor: cursor,
    };
  }

  editTag(flag: FlagTypeDto | null, activeFlagId: any) {
    this.activeFlagId = activeFlagId;
    console.log(flag);
    if (flag) {
      setTimeout(() => {
        const inputs = document.querySelectorAll(
          '.edit-input'
        ) as NodeListOf<HTMLInputElement>;
        inputs.forEach(input => {
          if (input.value.trim() === flag.name) {
            input.focus();
          }
        });
      }, 0);
    }
  }

  saveTag(flag: any) {
    this.activeFlagId = -1;
    this.alreadyUsedColors.push(this.selectedColor);
    if (this.selectedColor != '')
      this.flags.find(f => f.id == flag.id)!.color = this.selectedColor;
    this.selectedColor = '';

    this.alreadyUsedColors = this.flags.map(fg => fg.color);
    this.edit.emit(flag);
  }

  setCursorToEnd(event: any) {
    console.log(event);
    const range = document.createRange();
    const selection = window.getSelection();
    if (!selection) return;
    range.selectNodeContents(event.target);
    range.collapse(false);
    selection.removeAllRanges();
    selection.addRange(range);
  }

  selectColor(color: string, event: MouseEvent) {
    event.preventDefault();
    if (!this.alreadyUsedColors.includes(color)) this.selectedColor = color;
    setTimeout(() => {
      const input = document.querySelector('.edit-input') as HTMLInputElement;
      if (input) input.focus();
    }, 0);
  }

  deleteTagConfirm(flag: FlagTypeDto | null) {
    this.deleteConfirmationModalShow = false;
    console.log(flag);
    // this.flags = this.flags.filter(t => t !== flag);
    // console.log(flag)
    if (flag) this.delete.emit(flag);
  }

  deleteTag(flag: FlagTypeDto) {
    this.selectedFlag = flag;
    this.deleteConfirmationModalShow = true;
  }
}
