import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class FolderStateService {
  private folderId$ = new BehaviorSubject<number | null>(null);
  public readonly currentFolderId$ = this.folderId$.asObservable();

  setFolderId(id: number) {
    this.folderId$.next(id);
  }
}
