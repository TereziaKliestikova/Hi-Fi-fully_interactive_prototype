import { Component, Input } from '@angular/core';
import {
  BodySystemDescriptionDto,
  OrganDescriptionDto,
  PdfFile,
} from 'src/app/api/models';
import { StaticEndpoints } from 'src/types/StaticEndpoints.enum';

@Component({
  selector: 'app-description',
  templateUrl: './description.component.html',
  styleUrls: ['./description.component.scss'],
})
export class DescriptionComponent {
  @Input() description!: OrganDescriptionDto | BodySystemDescriptionDto;
  @Input() pdf?: PdfFile;
  @Input() staticIconEndpoint!: StaticEndpoints;
}
