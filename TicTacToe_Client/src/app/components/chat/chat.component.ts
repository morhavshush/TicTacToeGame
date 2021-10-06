import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChatService } from 'src/app/services/chatService/chat.service';
import { CookieService } from 'ngx-cookie';
import { OnlineUser } from 'src/app/models/online-user';
import { Router } from '@angular/router';
import { Message } from 'src/app/models/message';
import { RequestForPlay } from 'src/app/models/requestForPlay';
import { GameService } from 'src/app/services/gameService/game.service';
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
 
  currentUser: OnlineUser = new OnlineUser();
  msgInboxArray: Message[] = [];
  connectedUsers: OnlineUser[] = [];
  disconnectedUsers: OnlineUser[] = [];
  request: RequestForPlay;
  
  userNameReciever: string = '';
  userNameCookies: string = '';
  passwordCookies: string = '';
  req: string = '';
  answer: string = '';


  constructor(private cookieService: CookieService, public chatService: ChatService, private router: Router, private gameService: GameService) {
    this.userNameCookies = this.cookieService.get('UserName');
    this.passwordCookies = this.cookieService.get('Password');
    this.currentUser.userName = this.userNameCookies;
  }

  ngOnInit(): void {
    this.chatService.connectUserToChat(this.userNameCookies, this.passwordCookies).then(() => {});
    this.chatService.usersConnectedMappedObject().subscribe((data: OnlineUser[]) => { this.connectedUsers=data; });
    this.chatService.messageMappedObject().subscribe((data: Message) => { this.addToMessage(data); });
    this.chatService.usersDisconnectedMappedObject().subscribe((data: OnlineUser[]) => {  this.disconnectedUsers=data; });
    this.chatService.messagesMappedObject().subscribe((data: Message[]) => { this.msgInboxArray=data; });
    this.gameService.requestOfferMappedObject().subscribe((req: RequestForPlay) => { this.showReq(req) });
    this.gameService.requestRespondMappedObject().subscribe((req: RequestForPlay) => { this.showAnswerReq(req) });
  }

  //audio
  playMessageAudio() {
    let audio = new Audio();
    audio.src = "../../../assets/messagePop.mp3";
    audio.load();
    audio.play();
  }

  playRequestAudio() {
    let audio = new Audio();
    audio.src = "../../../assets/requestToPlay.wav";
    audio.load();
    audio.play();
  }


  //subscribe events
  showAnswerReq(req: RequestForPlay) {
    this.playRequestAudio();
    if (req.isAgree) {
      this.gameService.initGameOfUsers(req);
      this.router.navigate(['Board']);
    }
    else {
      this.answer = `${req.respondUserName} can't play with you now,maybe next time (:`;
    }
  }

  showReq(req: RequestForPlay) {
    this.playRequestAudio();
    this.req = `Hi ${req.respondUserName}! \n ${req.offerUserName} want to play with you, Do you want?`;
    this.request = req;
  }

  sendAnswer(canPlay: boolean) {
    if (canPlay) {
      this.request.isAgree = true;
      this.gameService.initGameOfUsers(this.request);
      this.router.navigate(['Board']);
    }
    else this.request.isAgree = false;
    this.gameService.sendAnswerOfRequest(this.request);
  }

  addToMessage(data: Message) {
    this.playMessageAudio();
    if (data.sender.userName == this.userNameReciever) {
      this.msgInboxArray.push(data);
    }
  }


  //chat service functions
  recieverUser(userNameReciever: string) {
    this.userNameReciever = userNameReciever;
    this.chatService.getAllMessages(this.userNameCookies, userNameReciever).then(() => { });
  }

  send() {
    if (this.currentUser) {
      if (this.currentUser.userName === null || this.currentUser.msgText.length === 0) {
        window.alert("Please insert a message");
        return;
      }
      else if(this.currentUser.userName===this.userNameReciever){
        window.alert("You can't send message to yourself");
        return;
      }
      else if (this.userNameReciever === null||this.userNameReciever==='') {
        window.alert("Please choose user from chat");
        return;
      } else {
        this.chatService.sendMessageToApi(this.currentUser, this.userNameReciever);
        this.currentUser.msgText = "";
      }
    }
  }

  logout() {
    this.chatService.logOutFromChat(this.userNameCookies, this.passwordCookies).then(() => { });
    this.router.navigate(['']);
  }

  inviteToPlay(userRespond: string) {
    this.gameService.sendRequestToPlay(userRespond,this.userNameCookies);
  }
}
