import { Component, inject } from '@angular/core';
import { CButtonComponent } from "../components/cbutton/cbutton.component";
import { AuthService } from '../services/auth-service';
import { Router } from '@angular/router';
import { CinputComponent } from "../components/cinput/cinput.component";
import { CreateLinkRequest, LinkService } from '../services/link-service';
import { ImageService } from '../services/image-service';
import { AddLinkDialogComponent } from "../components/add-link-dialog-component/add-link-dialog-component";
import { CommonModule } from '@angular/common';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SnackbarService } from '../services/snackbar.service';


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
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: true,
  imports: [CButtonComponent, CinputComponent, AddLinkDialogComponent, CommonModule, MatSnackBarModule]
})
export class HomeComponent {
  private readonly authService = inject(AuthService);
  private readonly linkService = inject(LinkService);
  private readonly imageService = inject(ImageService);
  private readonly router = inject(Router);

  private readonly snackbarService = inject(SnackbarService);

  links: LinkModel[] = [];
  showCreateLinkDialog:boolean = false;

  ngOnInit(){
    this._getLinks();
  }

  selectedMenu:number = 0;

  changeMenu(menuIndex: number) {
    this.selectedMenu = menuIndex;
    switch(menuIndex) {
      case 0:
        break;
      case 1:
        break;
      case 2:
        this.onLogout();
        break;
      default:
        break;
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
  
  private onLogout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
