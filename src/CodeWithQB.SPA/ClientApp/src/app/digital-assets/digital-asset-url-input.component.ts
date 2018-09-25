import {Component, ElementRef, AfterViewInit, Input, forwardRef} from "@angular/core";
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from "@angular/forms";
import { DigitalAssetService } from "./digital-asset.service";
import { switchMap } from "rxjs/operators";

@Component({
  templateUrl: "./digital-asset-url-input.component.html",
  styleUrls: ["./digital-asset-url-input.component.css"],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DigitalAssetInputUrlComponent),
      multi: true
    }
  ],
  selector: "app-digital-asset-input-url"
})
export class DigitalAssetInputUrlComponent implements ControlValueAccessor {
  constructor(
    private _elementRef: ElementRef,
    private _digitalAssetsService: DigitalAssetService) {
    this.onDragOver = this.onDragOver.bind(this);
    this.onDrop = this.onDrop.bind(this);
  }
  
  public get value() { return this.inputElement.value; }
    
  public writeValue(value: any): void { this.inputElement.value = value; }
  
  public registerOnChange(fn: any): void { this.onChangeCallback = fn; }
  
  public registerOnTouched(fn: any): void { this.onTouchedCallback = fn; }
  
  public setDisabledState?(isDisabled: boolean): void { this.inputElement.disabled = isDisabled; }
  
  public onTouchedCallback: () => void = () => { };
  
  public onChangeCallback: (_: any) => void = () => { };
  
  public ngAfterViewInit() {
    this._elementRef.nativeElement.addEventListener("dragover", this.onDragOver);
    this._elementRef.nativeElement.addEventListener("drop", this.onDrop, false);
  }
  
  public ngOnDestroy() {
    this._elementRef.nativeElement.removeEventListener("dragover", this.onDragOver);
    this._elementRef.nativeElement.removeEventListener("drop", this.onDrop, false);
  }
  
  public onDragOver(e: DragEvent) {
    e.stopPropagation();
    e.preventDefault();
  }
  
  public async onDrop(e: DragEvent) {
    e.stopPropagation();
    e.preventDefault();

    if (e.dataTransfer && e.dataTransfer.files) {
      const packageFiles = function (fileList: FileList) {
        let formData = new FormData();
        for (var i = 0; i < fileList.length; i++) {
          formData.append(fileList[i].name, fileList[i]);
        }
        return formData;
      }

      const data = packageFiles(e.dataTransfer.files);

      this._digitalAssetsService.upload({ data })
        .pipe(
          switchMap((x) => this._digitalAssetsService.getByIds({ digitalAssetIds: x.digitalAssetIds }))
        )
        .subscribe(x => {
        this.inputElement.value = x[0].relativePath;
        this.onChangeCallback(this.inputElement.value);
      });
    }
  }

  @Input()
  public placeholder: string;  
  
  public get inputElement(): HTMLInputElement { return this._elementRef.nativeElement.querySelector("input"); }
}
