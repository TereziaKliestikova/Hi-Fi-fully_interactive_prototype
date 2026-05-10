import {
  Directive,
  Input,
  OnChanges,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { AuthService } from 'src/app/oauth/auth.service';
import { RbacRoles } from 'src/types/RbacRoles.enum';

@Directive({
  selector: '[appHasAccess]',
})
// TODO remove logs
export class HasAccessDirective implements OnChanges {
  private authService: AuthService;
  private templateRef: TemplateRef<never>;
  private viewContainer: ViewContainerRef;

  @Input({ alias: 'appHasAccess', required: true }) allowedRoles!:
    | RbacRoles[]
    | undefined;
  // For attribute based access control we can add more properties here

  constructor(
    authService: AuthService,
    templateRef: TemplateRef<never>,
    viewContainer: ViewContainerRef
  ) {
    this.templateRef = templateRef;
    this.viewContainer = viewContainer;
    this.authService = authService;
  }

  ngOnChanges() {
    if (this.hasAccess())
      this.viewContainer.createEmbeddedView(this.templateRef);
    else this.viewContainer.clear();
  }

  private hasAccess = () => {
    const currentRole = this.getCurrentRole();
    if (this.allowedRoles === undefined || this.allowedRoles.length == 0) {
      return true;
    }
    return this.allowedRoles.includes(currentRole);
  };

  private getCurrentRole = () => {
    const token = this.authService.getAccessToken();
    const jwt = this.authService.parseJwt(token);
    return jwt.user_role;
  };
}
