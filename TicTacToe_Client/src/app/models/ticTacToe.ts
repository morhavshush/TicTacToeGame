import { OnlineUser } from "./online-user";

export class TicTacToe{
    public player1:OnlineUser;
    public player2:OnlineUser;
    public winner:OnlineUser;
    public isGameOver:boolean;
    public isDraw:boolean;
    public fields:string[]=[];
    public movesLeft:number; 
    public userNameTurn:string;
}