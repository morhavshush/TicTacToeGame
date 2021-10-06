import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpClient } from '@angular/common/http';
import { Subject, BehaviorSubject } from 'rxjs';
import { OnlineUser } from 'src/app/models/online-user';
import { CookieService } from 'ngx-cookie';
import { Message } from 'src/app/models/message';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService{
  
  connection: signalR.HubConnection;
  private message = new Subject<Message>();
  private allDisconnected = new BehaviorSubject<OnlineUser[]>([]);
  private allConnected = new BehaviorSubject<OnlineUser[]>([]);
  private allMessages = new BehaviorSubject<Message[]>([]);
  baseUrl =environment.baseUrl;

  constructor(private http: HttpClient, private cookiesService: CookieService) {
    this.start();
    this.connection.on("ConnectedUsers", (users: OnlineUser[]) => { this.allConnected.next(users); });
    this.connection.on("DisconnectedUsers", (users: OnlineUser[]) => { this.allDisconnected.next(users); });
    this.connection.on("AllMessages", (allMessages:Message[]) => { this.allMessages.next(allMessages); });
    this.connection.on("Message", (message:Message) => { this.message.next(message); });
  }

  //Mapped objects
  messageMappedObject(){
    return this.message.asObservable();
  }
  messagesMappedObject() {
    return this.allMessages.asObservable();
  }
  usersDisconnectedMappedObject() {
    return this.allDisconnected.asObservable();
  }
  usersConnectedMappedObject() {
    return this.allConnected.asObservable();
  }


  //start connection to server
  async start() {
    this.connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(this.baseUrl+"/chatsocket", { 
        skipNegotiation:true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();

    this.connection.start().then(() => {console.log("connction start");
 
    }).catch((err) => {
      console.log(err);
    }); 

  }

  //server's functions
  connectUserToChat(userName: string, password: string) {
    return this.connection.invoke("ConnectUserToChat", userName, password).catch(err => console.error(err));
  }
  getAllMessages(sender: string, reciever: string) {
    return this.connection.invoke("GetHistoryChat", sender, reciever).catch(err => console.error(err));
  }
  sendMessageToApi(msgDto: any, reciever: string) {
    this.connection.invoke("SendMessage", msgDto.userName, msgDto.msgText, reciever).catch(err => console.error(err));
  }
  isCanLogin(user: any) {
    try {
      return this.http.post(this.baseUrl+"/api/chat/login", user);
    } catch (error) {
      
      console.error(error);
    }
  }
  isCanRegister(user: any){
    try {
      return this.http.post(this.baseUrl+"/api/chat/register", user);
    } catch (error) {
      console.error(error);
    }
  }
  logOutFromChat(userName: string, password: string) {
    return this.connection.invoke('Logout', userName, password).catch(err => console.error(err));
  }
  sendDirectMessage(message: string, reciever: string): string {
    var curUser = this.cookiesService.get('UserName');
    this.connection.invoke('SendDirectMessage', curUser, message, reciever);
    return message;
  }

}
