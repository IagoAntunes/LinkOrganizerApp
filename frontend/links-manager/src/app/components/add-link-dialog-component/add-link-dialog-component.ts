import { Component, EventEmitter, Output } from '@angular/core';
import { CinputComponent } from "../cinput/cinput.component";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CButtonComponent } from "../cbutton/cbutton.component";

@Component({
  selector: 'app-add-link-dialog',
  templateUrl: './add-link-dialog-component.html',
  styleUrls: ['./add-link-dialog-component.scss'],
  standalone: true,
  imports: [CinputComponent, CommonModule, FormsModule, CButtonComponent],
})
export class AddLinkDialogComponent {
  @Output() closeDialog = new EventEmitter<void>();
  @Output() linkCreated = new EventEmitter<{ name: string; platform: string; url: string, file:File }>();

  name: string = '';
  platform: string = '';
  url: string = '';

  previewUrl: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;


  close() {
    this.closeDialog.emit();
  }

  onBackdrop(event: MouseEvent) {
    event.stopPropagation();
    this.close();
  }

  createLink() {
    if (this.name && this.platform && this.url && this.previewUrl) {
      this.linkCreated.emit({
        name: this.name,
        platform: this.platform,
        url: this.url,
        file: this.selectedFile!!,
      });
      this.close();
    }
  }

  onImageSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      const file = fileInput.files[0];
      this.selectedFile = file;
      const reader = new FileReader();
      reader.onload = (e) => {
        this.previewUrl = e.target?.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

}