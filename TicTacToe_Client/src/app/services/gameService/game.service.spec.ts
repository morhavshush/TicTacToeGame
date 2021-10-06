import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { CookieModule } from 'ngx-cookie';
import { ChatService } from '../chatService/chat.service';
import { GameService } from './game.service';


describe('GameService', () => {
  let service: GameService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientTestingModule,CookieModule.forRoot()],
      providers:[ChatService]
    });
    service = TestBed.inject(GameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

});

