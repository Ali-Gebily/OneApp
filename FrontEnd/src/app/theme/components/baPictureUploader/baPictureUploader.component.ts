import { Component, ViewChild, Input, Output, EventEmitter, ElementRef, Renderer } from '@angular/core'; 

@Component({
  selector: 'ba-picture-uploader',
  styles: [require('./baPictureUploader.scss')],
  template: require('./baPictureUploader.html')
})
export class BaPictureUploader {

  @Input() defaultPicture: string = '';
  @Input() picture: string = '';

  @Input() uploaderOptions: any = {};
  @Input() canDelete: boolean = true;
  @Output() filesSelected = new EventEmitter();

  onUpload: EventEmitter<any> = new EventEmitter();
  onUploadCompleted: EventEmitter<any> = new EventEmitter();

  @ViewChild('fileUpload') protected _fileUpload: ElementRef;

  public uploadInProgress: boolean = false;

  constructor(private renderer: Renderer) {
  }

  public ngOnInit(): void {
  }

  public onFiles(): void {
    let files = this._fileUpload.nativeElement.files;

    if (files.length) {
      const file = files[0];
      this._changePicture(file);

      // if (this._canUploadOnServer()) {
      //   this.uploadInProgress = true;
      //   console.log(file);
      //   //this._uploader.addFilesToQueue(files);
      // }
    }
    this.filesSelected.emit(files) 
  }

  public bringFileSelector(): boolean {
    this.renderer.invokeElementMethod(this._fileUpload.nativeElement, 'click');
    return false;
  }

  public removePicture(): boolean {
    this.picture = '';
     this.filesSelected.emit(null) 
    return false;
  }

  protected _changePicture(file: File): void {
    const reader = new FileReader();
    reader.addEventListener('load', (event: Event) => {
      this.picture = (<any>event.target).result;
    }, false);
    reader.readAsDataURL(file);
  }

  

 
}
