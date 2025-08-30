import { Component, inject } from '@angular/core';
import { AuthService } from '../services/auth-service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { LinksComponent } from "../links/links.component";
import { ProfileComponent } from "../profile/profile.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: true,
  imports: [CommonModule, MatSnackBarModule, LinksComponent, ProfileComponent]
})
export class HomeComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

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
  
  private onLogout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
