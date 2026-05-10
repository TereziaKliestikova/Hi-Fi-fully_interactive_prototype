import { booleanAttribute, Component, Input } from '@angular/core';

@Component({
  selector: 'app-text-with-link',
  templateUrl: './text-with-link.component.html',
  styleUrls: ['./text-with-link.component.scss'],
})
export class TextWithLinkComponent {
  @Input() text!: string;
  @Input() link!: { text: string; to: string };
  @Input({ transform: booleanAttribute }) linkLeft = false;
}
