import { Observable } from 'rxjs';
import { RbacRoles } from './RbacRoles.enum';

export type NavConfig = {
  text: Observable<string>;
  to: string;
  roles: RbacRoles[];
};
