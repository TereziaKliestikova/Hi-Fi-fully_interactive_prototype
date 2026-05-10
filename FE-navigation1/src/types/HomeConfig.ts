import { Observable } from 'rxjs';

export type TileConfig = {
  path: string;
  title: Observable<string>;
  description: Observable<string>;
  imageUrl: string;
  alt: string;
  heightOverride?: string;
  marginTopOverride?: string;
};
