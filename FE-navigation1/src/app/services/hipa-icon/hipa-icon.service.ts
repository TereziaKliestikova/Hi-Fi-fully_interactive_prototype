import { Injectable } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export class HipaIconService {
  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
  ) {}

  registerIcons(): void {
    this.registerIcon('hipa-add-folder', 'assets/admin/add-folder-icon.svg');
    this.registerIcon('hipa-folder', 'assets/admin/folder-icon.svg');
    this.registerIcon(
      'hipa-folder-simple',
      'assets/admin/folder-simple-icon.svg'
    );
    this.registerIcon('hipa-flag', 'assets/admin/flag-icon.svg');

    this.registerIcon('hipa-lock', 'assets/generic/lock-icon.svg');
    this.registerIcon('hipa-public', 'assets/generic/public-icon.svg');
    this.registerIcon('hipa-add', 'assets/generic/add-icon.svg');
    this.registerIcon('hipa-trash', 'assets/generic/trash-icon.svg');
    this.registerIcon(
      'hipa-add-folder-small',
      'assets/generic/add-folder-small-icon.svg'
    );
    this.registerIcon('hipa-add-file', 'assets/generic/add-file-icon.svg');
    this.registerIcon(
      'hipa-add-listitem',
      'assets/generic/add-listitem-icon.svg'
    );
  }

  registerIcon(name: string, path: string): void {
    this.matIconRegistry.addSvgIcon(
      name,
      this.domSanitizer.bypassSecurityTrustResourceUrl(path)
    );
  }
}
