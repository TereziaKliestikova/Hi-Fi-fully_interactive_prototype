import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ConfirmModalConfig } from '../../admin/modal-actions-confirmation/modal-actions-confirmation.config';
import { translate } from '@ngneat/transloco';

@Component({
  selector: 'app-folder-children-files',
  templateUrl: './folder-children-files.component.html',
  styleUrl: './folder-children-files.component.scss',
})
export class FolderChildrenFilesComponent {
  @Input() items: string[] = [];
  @Input() ids: number[] = [];
  @Input() iconType: 'icon' | 'img' = 'icon';
  @Input() icons: string | undefined;
  @Input() removable: boolean = false;
  @Output() remove = new EventEmitter<{ id: number; name: string }>();
  @Output() clicked = new EventEmitter<number>();

  showConfirmModal = false;
  modalConfig: ConfirmModalConfig | null = null;
  fileToDelete: number | null = null;
  itemToDelete: string | null = null;

  openConfirmDeleteModal(fileId: number, item: string) {
    this.modalConfig = {
      title: translate('learning.studyPage.actions.deleteFile'),
      paragraphs: [
        translate('learning.studyPage.dialog.confirmDeleteFileQuestion'),
      ],
      highlightText: item,
      cancelText: translate('learning.studyPage.actions.cancel'),
      confirmText: translate('learning.studyPage.actions.deleteFile'),
    };

    this.fileToDelete = fileId;
    this.itemToDelete = item;

    this.showConfirmModal = true;
  }
  removeItem(deleted: number) {
    this.showConfirmModal = false;
    return this.remove.emit({ id: deleted, name: this.itemToDelete! });
  }

  clickItem(clicked: number) {
    return this.clicked.emit(clicked);
  }

  getId(index: number): number {
    return this.ids?.[index];
  }
}
