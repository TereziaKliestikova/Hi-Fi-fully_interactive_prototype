import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-card-button',
  templateUrl: './card-button.component.html',
  styleUrls: ['./card-button.component.scss'],
})
export class CardButtonComponent {
  @Input() iconHeight: string = '90px';
  @Input() title: string = 'Organ';
  @Input() imageUrl: string = 'assets/home/organ.svg';
  @Input() outline: boolean = false;
  @Input() bold: boolean = false;
  @Input() imageOrder: 'first' | 'last' = 'last';
  @Input() fontSize: number = 27;
  @Input() size: 'large' | 'small' | 'nano' = 'large';
  @Input() color: string = '#155e75';
  @Input() backgroundColor: string = '#ffffff';
  @Input() disabled: boolean = false;
  @Input() width: number = 0;

  @Output() buttonClick = new EventEmitter<void>();
  onClick(): void {
    this.buttonClick.emit();
  }
}
