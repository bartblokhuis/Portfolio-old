import { Component, EventEmitter, forwardRef, Injector, NgZone, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { QuillModules } from 'ngx-quill';
import QuillType from 'quill';
import { GridPictureComponent } from 'src/app/modules/pictures/grid-picture/grid-picture.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-quill',
  templateUrl: './quill.component.html',
  styleUrls: ['./quill.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => QuillComponent),
        multi: true
  }]
})
export class QuillComponent implements OnInit {
  
  @Output() onEditorCreated: EventEmitter<any> = new EventEmitter();

  content: any = "";
  myToolbar= [
    ['bold', 'italic', 'underline', 'strike'],       
    ['blockquote', 'code-block'],

    [{ 'color': [] }, { 'background': [] }],         
    [{ 'font': [] }],
    [{ 'align': [] }],

    ['clean'],                                        
    ['image'] //add image here
  ];

  editorModules: QuillModules = {
    toolbar: {
      container: this.myToolbar,
      handlers: {
        image: this.imageHandler
      }
    }
  };

  quill: QuillType | null = null;

  constructor(private modalService: NgbModal, private injector: Injector) { }

  ngOnInit(): void {
  }

  imageHandler() {
    const zone = this.injector.get(NgZone);
    zone.run(() => {
      const modalRef = this.modalService.open(GridPictureComponent, { size: 'lg' });
      modalRef.componentInstance.modal = modalRef;
      modalRef.result.then((result?: any) => {
        if(result && this.quill && result.path){
          const cursorPosition = this.quill.getSelection()?.index ?? 0;
          var imgTag = `<img src="${environment.baseApiUrl + result.path}" title="${result.titleAttribute}" alt="${result.altAttribute} "/>`;
          this.quill.clipboard.dangerouslyPasteHTML(cursorPosition, imgTag);
          this.onChange(this.quill.root.innerHTML)
        }
      })
    })
  }

  ready(quill: QuillType) {
    this.quill = quill;
    let toolbar = quill.getModule('toolbar');
    toolbar.addHandler('image', this.imageHandler.bind(this));

    this.onEditorCreated.emit(quill);
  }

  propagateChange = (_: any) => {};
  registerOnChange(fn: any) {
    this.propagateChange = fn;
  }
  
  writeValue(value: string) {
     this.content = value; // <- you receive the value which is binded to this component here
  }

  onChange(event: string) {
     this.content = event;
     this.propagateChange(event); // <- you export the value so the binded variable is updated
  }
}
