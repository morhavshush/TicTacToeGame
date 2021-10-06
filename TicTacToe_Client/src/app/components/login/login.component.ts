import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from 'src/app/services/chatService/chat.service';
import { CookieService } from 'ngx-cookie';
import { OnlineUser } from 'src/app/models/online-user';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  user: OnlineUser;
  errorLogin: string;
  checking: boolean;

  constructor(private router: Router, private chatService: ChatService, private cookieService: CookieService) {
    this.user = new OnlineUser();
  }

  checkLogin() {
    this.checking = true;
    if (this.user.userName === null || this.user.password === null) {
      this.errorLogin = "Please insert all fields";
      this.checking = false;
      return;
    }
    this.cookieService.put('UserName', this.user.userName);
    this.cookieService.put('Password', this.user.password);
    this.chatService.isCanLogin(this.user).subscribe((user: OnlineUser) => {
      if (user === null) {
        this.checking = false;
        this.errorLogin = "User Name Or Password Not Current";
      }
      else if (user.connectionId !== null) {
        this.checking = false;
        this.errorLogin = "You Are Connected From Another Device";
      }
      else {
        this.router.navigate(['ChatPage']);
      }
    })

  }

  checkRegister() {
    this.checking = true;
    if (this.user.userName === null || this.user.password === null) {
      this.errorLogin = "Please insert all fields";
      this.checking = false;
      return;
    }
    this.cookieService.put('UserName', this.user.userName);
    this.cookieService.put('Password', this.user.password);
    this.chatService.isCanRegister(this.user).subscribe((user: OnlineUser) => {
      if (user === null) {
        this.checking = false;
        this.errorLogin = "Please enter differet values";
        return;
      }
      else {
        this.checkLogin();
      }
    })
  }
}
