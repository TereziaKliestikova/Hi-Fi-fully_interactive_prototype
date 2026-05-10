import {
  Component,
  Input,
  Output,
  OnInit,
  EventEmitter,
  OnDestroy,
} from '@angular/core';
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';

@Component({
  selector: 'app-admin-notification',
  templateUrl: './form-admin-notification.component.html',
  styleUrls: ['./form-admin-notification.component.scss'],
  animations: [
    trigger('fadeInOut', [
      state('void', style({ opacity: 0, transform: 'translateY(-10px)' })), // Начальное состояние (до появления)
      transition(':enter', [
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' })
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' })
        ),
      ]),
    ]),
  ],
})
export class FormAdminNotificationComponent implements OnInit, OnDestroy {
  @Input() message: string = '';
  @Input() duration: number = 5000;
  @Input() isUndoRequired: boolean = false;
  @Output() closed = new EventEmitter<string>();
  isVisible = false;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private timeoutId: any;

  ngOnInit(): void {
    this.show();
  }

  ngOnDestroy(): void {
    if (this.timeoutId) {
      clearTimeout(this.timeoutId);
    }
  }

  show(): void {
    this.isVisible = true;
    if (this.duration > 0) {
      this.timeoutId = setTimeout(() => {
        this.close('autoClose');
      }, this.duration);
    }
  }

  close(type: string): void {
    this.isVisible = false;
    setTimeout(() => {
      this.closed.emit(type);
    }, 300);
  }

  onUndo(): void {
    this.close('undo');
  }
}
