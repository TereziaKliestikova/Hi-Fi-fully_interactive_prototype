import { RbacRoles } from './RbacRoles.enum';

export type HipaJwt = {
  user_role: RbacRoles;
  sub: string | null; //user ID
  // Add more properties as needed
};
