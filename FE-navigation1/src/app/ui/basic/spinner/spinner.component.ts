import { Component, Input, numberAttribute } from '@angular/core';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.scss',
})
export class SpinnerComponent {
  @Input({ transform: numberAttribute }) size = 100;
}
