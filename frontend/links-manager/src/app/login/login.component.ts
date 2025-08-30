import { Component, inject } from '@angular/core';
import { CinputComponent } from "../components/cinput/cinput.component";
import { FormsModule } from '@angular/forms';
import { CButtonComponent } from "../components/cbutton/cbutton.component";
import { CommonModule } from '@angular/common';
import { AuthService, RegisterRequest } from '../services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [CinputComponent, CommonModule, FormsModule, CButtonComponent]
})
export class LoginComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);


  isRegister: boolean = false;
  loading = false;
  error = '';

  email: string = '';
  password: string = '';

  name: string = '';
  lastName: string = '';

  toggleForm() {
    this.isRegister = !this.isRegister;
    this.lastName = '';
    this.name = '';
  }

  resetForm(){
    this.email = '';
    this.password = '';
    this.name = '';
    this.lastName = '';
  }

  login(){
    this.loading = true;
    this.error = '';
    this.authService.login(this.email, this.password).subscribe(
      {
        next: (res) => {
          this.authService.setToken(res); 
          this.router.navigate(['/home']);
        },
        error: (e) => {
          console.error(e);
          this.error = 'Login failed. Please try again.';
        },
        complete: () => this.loading = false
      }
    );
  }

  register(){
    this.loading = true;
    this.error = '';
    const request: RegisterRequest = {
      name: this.name,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
      roles: ['Writer','Reader']
    }
    this.authService.register(request).subscribe(
      {
        next: (v) => {
          this.resetForm();
          this.toggleForm();
        },
        error: (e) => {
          console.error(e);
          this.error = 'Registration failed. Please try again.';
        },
        complete: () => this.loading = false
      }
    )
  }
}
