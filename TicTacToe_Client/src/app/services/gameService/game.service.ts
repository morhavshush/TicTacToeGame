import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { TicTacToe } from 'src/app/models/ticTacToe';
import { RequestForPlay } from 'src/app/models/requestForPlay';
import { ChatService } from '../chatService/chat.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  
  ticTacToe: TicTacToe;
  markerOrNot = new Subject<TicTacToe>();
  private requestOffer = new Subject<RequestForPlay>();
  private requestRespond = new Subject<RequestForPlay>();
  private isStartSub = new Subject<TicTacToe>();
  private winnerSub = new Subject<string>();


  constructor(private chatService: ChatService) {
    this.chatService.connection.on("AskForPlay", (req: RequestForPlay) => { this.requestOffer.next(req); });
    this.chatService.connection.on("RespondForPlay", (req: RequestForPlay) => { this.requestRespond.next(req); });
    this.chatService.connection.on("StartPlay", (play: TicTacToe) => { this.isStartSub.next(play); });
    this.chatService.connection.on("PlayerMarker", (playerMarker: TicTacToe) => { this.markerOrNot.next(playerMarker); });
    this.chatService.connection.on("PlayerNotMarker", (playerNotMarker: TicTacToe) => { this.markerOrNot.next(playerNotMarker); });
    this.chatService.connection.on("Winner", (winner: string) => { this.winnerSub.next(winner); });
  }

  //MappedObjects
  winnerMappedObject() {
    return this.winnerSub.asObservable();
  }
  playerMarkerMappedObject() {
    return this.markerOrNot.asObservable();
  }
  startedPlayRespondMappedObject() {
    return this.isStartSub.asObservable()
  }
  requestRespondMappedObject() {
    return this.requestRespond.asObservable();
  }
  requestOfferMappedObject() {
    return this.requestOffer.asObservable();
  }

  //chat service functions
  sendAnswerOfRequest(req: RequestForPlay) {
    this.chatService.connection.invoke("RespondForPlay", req).catch(err => console.error(err));
  }

  sendRequestToPlay(userRespond: string, userOffer: string) {
    this.chatService.connection.invoke("AskForPlay", userRespond, userOffer).catch(err => console.error(err));
  }

  initGameOfUsers(req: RequestForPlay) {
    this.chatService.connection.invoke("InitGame", req).catch(err => console.error(err));
  }

  sendPositionOfUser(game: TicTacToe) {
    this.chatService.connection.invoke("UserSendPosition", game).catch(err => console.error(err));
  }

}
