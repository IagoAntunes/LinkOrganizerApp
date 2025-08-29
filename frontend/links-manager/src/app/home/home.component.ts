import { Component, inject } from '@angular/core';
import { CButtonComponent } from "../components/cbutton/cbutton.component";
import { AuthService } from '../services/auth-service';
import { Router } from '@angular/router';
import { CinputComponent } from "../components/cinput/cinput.component";


interface LinkModel{
  id:string,
  name:string,
  platform:string,
  url:string,
  imageId:string,
};

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: true,
  imports: [CButtonComponent, CinputComponent]
})
export class HomeComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  links: LinkModel[] = [
    {
      id: '1',
      name: 'Link 1',
      platform: 'Platform 1',
      url: 'https://www.example.com/1',
      imageId: 'image1.png'
    },
    {
      id: '2',
      name: 'Link 2',
      platform: 'Platform 2',
      url: 'https://www.example.com/2',
      imageId: 'image2.png'
    },
    {
      id: '3',
      name: 'Link 3',
      platform: 'Platform 3',
      url: 'https://www.example.com/3',
      imageId: 'image3.png'
    }
  ];


  selectedMenu:number = 0;

  changeMenu(menuIndex: number) {
    this.selectedMenu = menuIndex;
    switch(menuIndex) {
      case 0:
        break;
      case 1:
        break;
      case 2:
        this.logout();
        break;
      default:
        break;
    }
  }

  addLink(){
    //
  }

  deleteLink(id:string){
    console.log(id);
  }
  
  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
