import { Component, Input, EventEmitter, Output } from '@angular/core';
@Component({
  selector: 'app-add-flags',
  templateUrl: './add-flags.component.html',
  styleUrls: ['./add-flags.component.scss'],
})
export class AddFlagsComponent {
  @Input() alreadyUsedColors: string[] = [];

  @Output() closeComponent = new EventEmitter<void>();
  @Output() create = new EventEmitter<{ name: string; color: string }>();

  labelName: string = '';
  selectedColor: string = '';
  showWarning: boolean = false;
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

  setColor(color: string) {
    let actualColor = color;
    let actualSize = '28px';
    let cursor = 'pointer';
    if (this.alreadyUsedColors.includes(color)) {
      actualColor = this.enableColors[this.colors.indexOf(color)];
      actualSize = '25px';
      cursor = 'default';
    }

    return {
      'background-color': actualColor,
      width: actualSize,
      height: actualSize,
      cursor: cursor,
    };
  }

  colorNotUsed(color: string) {
    if (!this.alreadyUsedColors.includes(color)) return true;
    else return false;
  }

  selectColor(color: string) {
    console.log(color);
    if (!this.alreadyUsedColors.includes(color)) this.selectedColor = color;
  }

  closeModal() {
    this.closeComponent.emit();
  }

  createLabel() {
    if (this.labelName && this.selectedColor) {
      this.create.emit({ name: this.labelName, color: this.selectedColor });
      this.closeModal();
    } else {
      this.showWarning = true;
    }
  }
}
