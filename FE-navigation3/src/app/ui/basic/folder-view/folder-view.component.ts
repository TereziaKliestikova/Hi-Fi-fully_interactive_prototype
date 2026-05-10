import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
import { MatMenu } from '@angular/material/menu';
import { DirectoryTreeDto } from '../../../api/models/directory-tree-dto';
import { LearningService } from '../../../api/services/learning.service';
import { translate } from '@ngneat/transloco';
import { ConfirmModalConfig } from '../../admin/modal-actions-confirmation/modal-actions-confirmation.config';

interface FlatNode {
  id: number;
  expandable: boolean;
  name: string;
  public: boolean;
  level: number;
}

interface IModal {
  name: string;
  open: boolean;
  modalConfig: ConfirmModalConfig;
  createAction?: (args: unknown) => void;
  confirmAction?: () => void;
}

@Component({
  selector: 'app-folder-view',
  templateUrl: './folder-view.component.html',
  styleUrl: './folder-view.component.scss',
})
export class FolderViewComponent implements OnChanges {
  menuTargetNode: FlatNode | null = null;
  selectedFolder: FlatNode | null = null;

  // modalConfig: ConfirmModalConfig | null = null;
  // showFileNameModal: boolean = false;

  expandedNodeIds: Set<number> = new Set<number>();

  modals: { [key: string]: IModal } | null = null;
  currentlyOpenModal: IModal | null = null;

  @Output() folderSelectionChanged = new EventEmitter<{
    name: string;
    id: number;
  } | null>();

  @Output() foldersChanged = new EventEmitter();
  @Output() folderDetailsChanged = new EventEmitter();
  @Output() folderDeleted = new EventEmitter<string>();
  @Output() folderPublished = new EventEmitter<string>();
  @Output() folderHidden = new EventEmitter<string>();

  @Input() data?: DirectoryTreeDto[];
  @Input() studyName?: string | null;
  @Input() selectedFolderId?: number;
  @Input() accessType: boolean = false;

  @ViewChild('nodeMenu') nodeMenu!: MatMenu;

  constructor(private learningService: LearningService) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['data']) {
      if (changes['data'].currentValue) {
        // Save the current expansion state
        this.saveExpandedNodesState();

        // Update the data source
        this.dataSource.data = changes['data'].currentValue;

        // Restore the expansion state
        setTimeout(() => {
          this.restoreExpandedNodesState();

          // If we have a selected folder ID, make sure it's visible
          if (this.selectedFolderId) {
            const node = this.treeControl.dataNodes.find(
              n => n.id === this.selectedFolderId
            );
            if (node) {
              this.selectFolder(node);
            }
          }
        });
      }
    }

    if (
      changes['selectedFolderId'] &&
      changes['selectedFolderId'].currentValue
    ) {
      if (this.data === undefined) {
        return;
      }

      const folderId = changes['selectedFolderId'].currentValue;

      const node = this.treeControl.dataNodes.find(n => n.id === folderId);
      if (node) {
        this.selectFolder(node);
      }
    }
  }

  // Data transformation
  private _transformer = (node: DirectoryTreeDto, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      id: node.id!,
      name: node.name!,
      public: node.isPublic!,
      level: level!,
    };
  };

  treeControl = new FlatTreeControl<FlatNode>(
    node => node.level,
    node => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.children
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: FlatNode) => node.expandable;
  hasNoChild = (_: number, node: FlatNode) => !node.expandable;

  selectFolder(node: FlatNode): void {
    this.selectedFolder = node;
    console.log(node);
    this.expandParents(node);
    this.folderSelectionChanged.emit(node);
  }

  private saveExpandedNodesState(): void {
    // Clear the existing set
    this.expandedNodeIds.clear();

    // Save all currently expanded node IDs
    this.treeControl.dataNodes?.forEach(node => {
      if (this.treeControl.isExpanded(node)) {
        this.expandedNodeIds.add(node.id);
      }
    });
  }

  private restoreExpandedNodesState(): void {
    // Restore expansion state for nodes that were previously expanded
    this.treeControl.dataNodes?.forEach(node => {
      if (this.expandedNodeIds.has(node.id)) {
        this.treeControl.expand(node);
      }
    });
  }

  private expandParents(node: FlatNode): void {
    if (node.level === 0) {
      return;
    }

    // Get all nodes from the tree
    const allNodes = this.treeControl.dataNodes;
    const nodeIndex = allNodes.indexOf(node);

    if (nodeIndex === -1) {
      return; // Node not found in the tree
    }

    // Create a map to track the parent at each level
    const parentsMap = new Map<number, FlatNode>();

    // Start from the node's index and go backwards to find all parents
    for (let i = nodeIndex; i >= 0; i--) {
      const currentNode = allNodes[i];

      // Only consider nodes with level less than our target node
      if (currentNode.level < node.level) {
        // If we don't have a parent for this level yet, and it's one level above the next level we need
        // (either our node's level or the level of the deepest parent we've found so far)
        const nextLevelNeeded =
          parentsMap.size > 0
            ? Math.min(...Array.from(parentsMap.keys())) - 1
            : node.level - 1;

        if (currentNode.level === nextLevelNeeded) {
          // This is a direct parent of either our node or another parent
          parentsMap.set(currentNode.level, currentNode);

          // If we found the root parent (level 0), we can stop
          if (currentNode.level === 0) {
            break;
          }
        }
      }
    }

    // Expand all parents from top to bottom
    Array.from(parentsMap.entries())
      .sort((a, b) => a[0] - b[0]) // Sort by level (ascending)
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      .forEach(([_, parentNode]) => {
        this.treeControl.expand(parentNode);
      });
  }
  createFolder(newFolderName: string): void {
    const newFolderBody: { name: string } = {
      name: newFolderName,
    };

    if (this.menuTargetNode) {
      this.learningService
        .learningDirectoryIdNewPost$Json({
          id: this.menuTargetNode!.id,
          body: newFolderBody,
        })
        .subscribe(response => {
          const newDirectoryId = response.directoryId;
          this.selectedFolderId = newDirectoryId;

          this.foldersChanged.emit();
        });
    }
  }

  deleteFolder(folderId: number, folderName: string): void {
    this.learningService
      .learningDirectoryIdDelete({ id: folderId })
      .subscribe(() => {
        this.foldersChanged.emit();
        this.folderDeleted.emit(folderName);
      });

    if (this.selectedFolderId === folderId) {
      this.selectedFolderId = undefined;
      this.folderSelectionChanged.emit(null);
    }
  }

  getIconSizeForLeve(level: number) {
    switch (level) {
      case 0:
        return 29;
      case 1:
        return 25;
      case 2:
        return 21;
    }
    return 25;
  }

  private toggleVisibilityState(node: FlatNode) {
    this.learningService
      .learningDirectoryIdVisibilityPatch({
        id: node.id,
        body: {
          isPublic: !node.public,
        },
      })
      .subscribe(() => {
        if (!node.public) {
          this.folderPublished.emit(node.name);
        } else {
          this.folderHidden.emit(node.name);
        }
        this.folderDetailsChanged.emit(node.id);
        node.public = !node.public;
      });
  }

  nodeMenuActions(action: string, node: FlatNode): void {
    switch (action) {
      case 'hide':
        if (node.public) {
          this.toggleVisibilityState(node);
        } else {
          this.createAndOpenConfirmPublishModal(node);
        }
        break;
      case 'add':
        this.openModal('Create');
        console.log('Add new item under:', node);
        break;
      case 'delete':
        this.createAndOpenDeleteFolderModal(node.id, node.name);
        break;
    }
  }

  openModal(modalType: string) {
    if (modalType == 'Create') {
      this.createAndOpenCreateFolderModal();
    }
  }

  private createAndOpenDeleteFolderModal(nodeId: number, folderName: string) {
    this.modals = {
      Delete: {
        name: 'Delete',
        open: true,
        modalConfig: {
          title: translate('learning.studyPage.actions.deleteFolder'),
          paragraphs: [
            translate('learning.studyPage.dialog.confirmDeleteFolderQuestion'),
          ],
          highlightText: folderName,
          confirmText: translate('learning.studyPage.actions.deleteFolder'),
          cancelText: translate('learning.studyPage.actions.cancel'),
        },
        confirmAction: () => {
          this.deleteFolder(nodeId, folderName);
          this.modals!['Delete'].open = false;
        },
      },
    };
    this.currentlyOpenModal = this.modals!['Delete'];
  }
  private createAndOpenConfirmPublishModal(node: FlatNode) {
    this.learningService
      .learningDirectoryIdParentsGet$Json({
        id: node!.id!,
      })
      .subscribe(data => {
        const folderNamesString = data.map(d => d.name as string).join(', ');
        let privateParentsText = '';
        if (folderNamesString) {
          privateParentsText =
            translate('learning.studyPage.dialog.privateParents') + ':';
        }

        this.modals = {
          ...this.modals,
          ConfirmPublish: {
            name: 'ConfirmPublish',
            open: true,
            modalConfig: {
              title: translate('learning.studyPage.actions.publishFolder'),
              paragraphs: [
                translate('learning.studyPage.dialog.confirmPublishQuestion'),
                privateParentsText,
              ],
              highlightText: folderNamesString,
              cancelText: translate('learning.studyPage.actions.cancel'),
              confirmText: translate(
                'learning.studyPage.actions.publishFolder'
              ),
            },
            confirmAction: () => {
              this.toggleVisibilityState(node);
              this.modals!['ConfirmPublish'].open = false;
            },
          },
        };
        this.currentlyOpenModal = this.modals!['ConfirmPublish'];
      });
  }

  private createAndOpenCreateFolderModal() {
    this.modals = {
      Create: {
        name: 'Create',
        open: true,
        modalConfig: {
          title: translate('learning.studyPage.actions.newFolder'),
          confirmText: translate('learning.studyPage.actions.createNewFolder'),
          cancelText: translate('learning.studyPage.actions.cancel'),
          textBoxPlaceholderText: translate(
            'learning.studyPage.actions.newFolder'
          ),
        },
        createAction: (args: unknown) => {
          this.createFolder(args as string);
          this.modals!['Create'].open = false;
        },
      },
    };
    this.currentlyOpenModal = this.modals!['Create'];
  }
}
