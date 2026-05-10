import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-file-modal',
  templateUrl: './add-file-modal.component.html',
  styleUrl: './add-file-modal.component.scss',
})
export class AddFileModalComponent {
  selectedFile: File | null = null;
  touched = false;

  constructor(
    private dialogRef: MatDialogRef<AddFileModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { labelkey: string; text: string }
  ) {}

  onInputClicked(): void {
    // this.touched = true;
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.selectedFile = input.files?.[0] ?? null;
    this.touched = false;
  }

  close(): void {
    this.dialogRef.close();
  }

  submit(): void {
    if (this.selectedFile) {
      this.dialogRef.close(this.selectedFile);
    } else {
      this.touched = true;
    }
  }
}
