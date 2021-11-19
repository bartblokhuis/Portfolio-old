import { AfterContentChecked, ChangeDetectorRef } from '@angular/core';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms'

@Component({
  selector: 'app-rich-text-editor',
  templateUrl: './rich-text-editor.component.html',
  styleUrls: ['./rich-text-editor.component.scss']
})
export class RichTextEditorComponent implements AfterContentChecked {

  @Input() control: FormControl
  @Output() onContentChanged = new EventEmitter()

  constructor(private cdref: ChangeDetectorRef) { }
  
  ngOnInit(): void {
  }

  onChange($event) {
    this.onContentChanged.emit($event);
  }

  /**
   * Prevents an console error
   * Solution found at:
   * https://stackoverflow.com/questions/45467881/expressionchangedafterithasbeencheckederror-expression-has-changed-after-it-was
   */
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

}
