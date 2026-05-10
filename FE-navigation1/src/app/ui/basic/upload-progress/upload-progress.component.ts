import { Component, OnInit } from '@angular/core';
import {
  UploadService,
  UploadTask,
} from 'src/app/services/sample-image_upload/sample-image-upload';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-upload-progress',
  templateUrl: './upload-progress.component.html',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatIconModule,
    TranslocoModule,
  ],
  styleUrl: './upload-progress.component.scss',
  animations: [
    trigger('slideDown', [
      state(
        'open',
        style({
          height: '*',
          opacity: 1,
          overflow: 'hidden',
        })
      ),
      state(
        'closed',
        style({
          height: '0px',
          opacity: 0,
          overflow: 'hidden',
        })
      ),
      transition('open => closed', [animate('300ms ease-in-out')]),
      transition('closed => open', [animate('300ms ease-in-out')]),
    ]),
    trigger('fadeInOut', [
      state(
        'visible',
        style({
          opacity: 1,
          height: '*',
          overflow: 'hidden',
        })
      ),
      state(
        'hide',
        style({
          opacity: 0,
          height: '0px',
          overflow: 'hidden',
          display: 'none',
        })
      ),
      transition('visible => hide', [animate('300ms ease-in-out')]),
      transition('hide => visible', [animate('300ms ease-in-out')]),
    ]),
  ],
})
export class UploadProgressComponent implements OnInit {
  tasks: UploadTask[] = [];
  isExpanded = true;
  isOpen = true;
  constructor(private uploadService: UploadService) {}

  ngOnInit() {
    const storedOpen = sessionStorage.getItem('uploadProgress_isOpen');
    const storedExpanded = sessionStorage.getItem('uploadProgress_isExpanded');

    this.isOpen = storedOpen !== null ? JSON.parse(storedOpen) : true;
    this.isExpanded =
      storedExpanded !== null ? JSON.parse(storedExpanded) : true;

    this.uploadService.getTasks().subscribe(tasks => {
      this.tasks = tasks;
      console.log('Updated tasks from progress subscriber:', this.tasks);
    });
    this.isExpanded = true;
    this.isOpen = true;
  }

  toggleList() {
    this.isExpanded = !this.isExpanded;
    sessionStorage.setItem(
      'uploadProgress_isExpanded',
      JSON.stringify(this.isExpanded)
    );
  }

  closeList() {
    this.isOpen = false;
    sessionStorage.setItem(
      'uploadProgress_isOpen',
      JSON.stringify(this.isOpen)
    );
  }

  getTasksProgress(): number {
    const filteredTasks = this.tasks.filter(task => task.status !== 'canceled');
    return (
      filteredTasks.reduce((sum, task) => sum + task.progress, 0) /
      filteredTasks.length
    );
  }

  cancelUpload(task: UploadTask) {
    this.uploadService.cancelTask(task);
  }

  clearTasks() {
    this.tasks = [];
    this.isOpen = false;
    sessionStorage.setItem(
      'uploadProgress_isOpen',
      JSON.stringify(this.isOpen)
    );
  }
}
