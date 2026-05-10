import { booleanAttribute, Component, Input } from '@angular/core';

@Component({
  selector: 'app-anchor',
  templateUrl: './anchor.component.html',
  styleUrls: ['./anchor.component.scss'],
})
export class AnchorComponent {
  @Input() to!: string;
  @Input({ transform: booleanAttribute }) externalLink = false;
  @Input({ transform: booleanAttribute }) noUnderline = false;
}
