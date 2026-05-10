import { AdminService } from './../../api/services/admin.service';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, lastValueFrom } from 'rxjs';
import * as tus from 'tus-js-client';
import { environment } from '../../../environments/environment';
import { AdminUploadSampleImageDataPost$Plain$Params } from 'src/app/api/fn/admin/admin-upload-sample-image-data-post-plain';

export type UploadStatus =
  | 'pending'
  | 'uploading'
  | 'completed'
  | 'canceled'
  | 'failed';

export interface UploadTask {
  file: File;
  progress: number;
  status: UploadStatus;
  uploader?: tus.Upload;
}

@Injectable({
  providedIn: 'root',
})
export class UploadService {
  private uploadURL = `${environment.apiUrl}${environment.upload}`;
  private uploads = new BehaviorSubject<UploadTask[]>([]);

  constructor(private adminService: AdminService) {}

  private initTask(file: File): UploadTask {
    const task: UploadTask = {
      file,
      progress: 0,
      status: 'pending',
    };
    this.uploads.next([...this.uploads.getValue(), task]);
    return task;
  }

  private updateTasks(): void {
    this.uploads.next([...this.uploads.getValue()]);
  }

  getTasks(): Observable<UploadTask[]> {
    return this.uploads.asObservable();
  }

  cancelTask(task: UploadTask): void {
    if (task.uploader) {
      task.uploader.abort();
    }
    task.status = 'canceled';
    this.updateTasks();
  }

  private async startUpload(
    task: UploadTask,
    metadataOther: Record<string, string>,
    onSuccessCallback: () => void = () => {}
  ): Promise<void> {
    task.status = 'uploading';

    const upload = new tus.Upload(task.file, {
      endpoint: this.uploadURL,
      chunkSize: 1 * 1024 * 1024,
      retryDelays: [0, 1000, 3000, 5000],
      metadata: {
        name: task.file.name,
        contentType: task.file.type || 'application/octet-stream',
        ...metadataOther,
      },
      uploadSize: task.file.size,
      onProgress: (uploaded, total) => {
        task.progress = (uploaded / total) * 100;
        this.updateTasks();
      },
      onError: err => {
        task.status = 'failed';
        this.updateTasks();
        console.error('Upload error:', err);
      },
      onSuccess: () => {
        task.status = 'completed';
        this.updateTasks();
        onSuccessCallback();
      },
    });

    task.uploader = upload;
    upload.start();
  }

  async uploadCaustry(file: File, sampleId: number): Promise<void> {
    const task = this.initTask(file);
    await this.startUpload(task, {
      fileType: 'caustry',
      sampleId: sampleId.toString(),
    });
  }

  async uploadFolderFile(
    file: File,
    folderId: number,
    onSuccessCallback: () => void
  ): Promise<void> {
    const task = this.initTask(file);
    await this.startUpload(
      task,
      {
        fileType: 'folderFile',
        folderId: folderId.toString(),
      },
      onSuccessCallback
    );
  }

  async uploadSample(
    formData: AdminUploadSampleImageDataPost$Plain$Params,
    file: File
  ): Promise<void> {
    const groupId = await lastValueFrom(
      this.adminService.adminUploadSampleImageDataPost$Plain(formData)
    );
    const task = this.initTask(file);
    await this.startUpload(task, {
      fileType: 'sampleImage',
      groupId: groupId || '0',
    });
  }

  clearTasks(): void {
    this.uploads.next([]);
  }
}
