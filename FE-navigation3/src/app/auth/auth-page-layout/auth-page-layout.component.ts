import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-auth-page-layout',
  templateUrl: './auth-page-layout.component.html',
  styleUrls: ['./auth-page-layout.component.scss'],
})
export class AuthPageLayoutComponent {
  @Input() title!: string;
}
