import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import Quill from 'quill';

@Component({
  selector: 'app-text-editor',
  templateUrl: './text-editor.component.html',
  styleUrls: ['./text-editor.component.scss'],
})
export class TextEditorComponent implements AfterViewInit {
  @Input() content: string | null | undefined = undefined;
  // because of dynamic component creation transloco does not work in the html template
  // I had to send all the necessary translations in a dictionary from the parent
  @Input() translations: { [key: string]: string } = {};

  @Output() contentSaved = new EventEmitter<string>();
  @Output() editorClosed = new EventEmitter<void>();

  textEditor: Quill | undefined;

  ngAfterViewInit() {
    this.textEditor = new Quill('#editor-container', {
      theme: 'snow',
      modules: {
        toolbar: {
          container: '#editor-toolbar',
        },
      },
    });

    if (this.content) {
      this.textEditor.root.innerHTML = this.content;
    }
  }

  saveContent() {
    const content = this.textEditor?.root.innerHTML ?? '';
    this.contentSaved.emit(content);
  }

  cancel() {
    this.editorClosed.emit();
  }
}
