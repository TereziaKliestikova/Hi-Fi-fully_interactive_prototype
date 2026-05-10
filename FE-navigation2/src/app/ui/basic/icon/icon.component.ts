import { Component, Input, OnInit, SecurityContext } from '@angular/core';
import { AssetsService } from 'src/app/api/services';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { StaticEndpoints } from 'src/types/StaticEndpoints.enum';
import { IconPathDto } from 'src/app/api/models';

@Component({
  selector: 'app-icon',
  templateUrl: './icon.component.html',
  styleUrl: './icon.component.scss',
})
export class IconComponent implements OnInit {
  @Input() iconId!: number;
  @Input() endpoint!: string;

  iconUrl: string | null = null;

  constructor(
    private assetsService: AssetsService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.fetchIcon(this.iconId);
  }

  fetchIcon(iconId: number) {
    switch (this.endpoint) {
      case StaticEndpoints.BodySystemIcons:
        this.assetsService
          .assetsBodySystemIconPathIdGet$Json({ id: iconId })
          .subscribe(data => {
            this.assignIcon(data);
          });
        break;
      case StaticEndpoints.OrganIcons:
        this.assetsService
          .assetsOrganIconPathIdGet$Json({ id: iconId })
          .subscribe(data => {
            this.assignIcon(data);
          });
        break;
      default:
        console.error('Invalid endpoint in icon.component.ts');
        break;
    }
  }

  assignIcon(data: IconPathDto) {
    const tempUrl = environment.apiUrl + '/' + data.iconPath;
    this.iconUrl = this.sanitizer.sanitize(SecurityContext.URL, tempUrl);
  }
}
