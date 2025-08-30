import { Component, inject } from '@angular/core';
import { CinputComponent } from '../components/cinput/cinput.component';
import { AuthService } from '../services/auth-service';

interface UserInfo {
  name:string,
  email:string,
}


@Component({
  selector: 'app-profile',
  imports: [CinputComponent],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {

  private readonly authService = inject(AuthService);
  userInfo: UserInfo | null = null;

  ngOnInit() {
    this._getUserInfo();
  }

  private _getUserInfo(){
    this.authService.getUserData().subscribe(
      {
        next: (data : UserInfo) => {
          this.userInfo = data;
        },
        error: () => {
          console.error('Error retrieving user info');
        },
        complete: () => {
          console.log('User info retrieval complete');
        }
      }
    );
  }

}
