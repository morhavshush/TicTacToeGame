import { OnlineUser } from "./online-user";

export class Message {
  public id = '';
  public sender:OnlineUser;
  public reciever:OnlineUser;
  public messageText = '';
  public timeSent:Date;
  }
  