import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie';
import { TicTacToe } from 'src/app/models/ticTacToe';
import { GameService } from 'src/app/services/gameService/game.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent{
  game: TicTacToe;
  myTurn: boolean;
  userName: string;
  message: string;

  constructor(private gameService: GameService, private cookieService: CookieService, private router: Router) {
    this.userName = this.cookieService.get('UserName');
    this.game=new TicTacToe();
    this.game.fields = [];
    this.gameService.startedPlayRespondMappedObject().subscribe((data: TicTacToe) => { this.game = data; });
    this.gameService.playerMarkerMappedObject().subscribe((game: TicTacToe) => { this.renderGame(game); });
    this.gameService.winnerMappedObject().subscribe((winner:string)=>{this.showWinner(winner)});
  }
 
  showWinner(winner: string) {
    this.winPlayAudio();
    window.alert(`${winner} WIN !!!`);
    this.router.navigate(['ChatPage']);
  }

  renderGame(game: TicTacToe) {
    if(this.game.movesLeft <= 1) {
      window.alert(`GAME OVER! NOBODY WIN ..`);
      this.router.navigate(['ChatPage']);
    }
    this.game = game;
  }

  clickPosition(position: number) {
    this.playerClickAudio();
    if (this.userName === this.game.player1.userName) {
      this.game.fields[position] = this.game.player1.type;
    }
    else {
      this.game.fields[position] = this.game.player2.type;
    }
    this.gameService.sendPositionOfUser(this.game);
  }

  //Audio
  winPlayAudio(){
    let audio = new Audio();
    audio.src = "../../../assets/winSound.wav";
    audio.load();
    audio.play();
  }
  playerClickAudio(){
    let audio1 = new Audio();
    audio1.src = "../../../assets/playerClick.wav";
    audio1.load();
    audio1.play();
  }


}
