import { Component } from '@angular/core';
import { CookieService } from 'ngx-cookie';
import { ChatService } from 'src/app/services/chatService/chat.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  title = 'web-chat-app';
  constructor(private cookieService:CookieService,private chatService:ChatService) {

  }
  ngOnDestroy(): void {
    var userName = this.cookieService.get('UserName');
    var password = this.cookieService.get('Password');
    this.chatService.logOutFromChat(userName, password).then(() => { });
  }
}
