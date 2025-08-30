import { Component, inject } from '@angular/core';
import { CButtonComponent } from '../components/cbutton/cbutton.component';
import { CinputComponent } from '../components/cinput/cinput.component';
import { AddLinkDialogComponent } from '../components/add-link-dialog-component/add-link-dialog-component';
import { ImageService } from '../services/image-service';
import { CreateLinkRequest, LinkService } from '../services/link-service';
import { SnackbarService } from '../services/snackbar.service';
import { CommonModule } from '@angular/common';

interface LinkModel{
  id:string,
  name:string,
  platform:string,
  url:string,
  imageId:string,
  user: {
    id:string,
    name:string,
    email:string,
  },
  imageUrl?: string,
};

@Component({
  selector: 'app-links',
  imports: [CButtonComponent, AddLinkDialogComponent, CommonModule],
  templateUrl: './links.component.html',
  styleUrl: './links.component.scss'
})
export class LinksComponent {
  links: LinkModel[] = [];
  showCreateLinkDialog:boolean = false;
  private readonly linkService = inject(LinkService);
  private readonly imageService = inject(ImageService);

  private readonly snackbarService = inject(SnackbarService);

  ngOnInit(){
    if(this.links.length == 0){
      this._getLinks();
    }
  }

  private _getLinks(){
    this.linkService.getLinks().subscribe(
      {
        next: (links: LinkModel[]) => {
          this.links = links;
          this.links.forEach(link => {
            if (link.imageId) {
              this.imageService.getImage(link.imageId).subscribe(
                {
                  next: (blob: Blob) => {
                    link.imageUrl = URL.createObjectURL(blob);
                  },
                  error: (error) => {
                    link.imageUrl = 'assets/default-image.png';
                  },
                  complete: () => {
                    console.log('Image fetching complete');
                  }
                }
              );
            } else {
              link.imageUrl = 'assets/default-image.png';
            }
          });
            this.snackbarService.success('Links fetched successfully!');
        },
        error: (e) => {
          console.error('Error fetching links:', e);
          this.snackbarService.error(e.message || 'Error fetching links');
        },
        complete: () => {
          console.log('Link fetching complete');
        }
      }
    );
  }

  openCreateLinkDialog(){
    this.showCreateLinkDialog = true;
    console.log(this.showCreateLinkDialog);
  }

  onDialogClose(){
    this.showCreateLinkDialog = false;
  }

  onLinkCreated(linkData: { name: string; platform: string; url: string; file: File }) {
    const request: CreateLinkRequest = {
      name: linkData.name,
      platform: linkData.platform,
      url: linkData.url,
      file: linkData.file
    };

    this.linkService.createLink(request).subscribe(
      {
        next:(response: LinkModel) => {
          this.showCreateLinkDialog = false;
          this._getLinks();
        },
        error: (e) => {
          this.snackbarService.error(e.message || 'Error creating link');
        },
        complete: () => {
          console.log('Link creation complete');
        }
      }
    )

  }

  onDeleteLink(id:string){
    this.linkService.deleteLink(id).subscribe(
      {
        next:(response) => {
          this._getLinks();
        },
        error: (e) => {
          this.snackbarService.error(e.message || 'Error deleting link');
        },
        complete: () => {
          console.log('Link deletion complete');
        }
      }
    )
  }

  getPlatformClass(platform: string): string {
    switch ((platform || '').toLowerCase()) {
      case 'prime vídeo': return 'chip-prime';
      case 'max':         return 'chip-max';
      case 'netflix':     return 'chip-netflix';
      case 'disney+':     return 'chip-disney';
      default:            return 'chip-default';
    }
  }

}
