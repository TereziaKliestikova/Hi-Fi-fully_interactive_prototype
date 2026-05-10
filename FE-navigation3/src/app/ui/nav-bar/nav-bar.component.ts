import { Component, Input } from '@angular/core';
import { NavConfig } from 'src/types/NavConfig.type';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  @Input() navConfig!: NavConfig[];
}
