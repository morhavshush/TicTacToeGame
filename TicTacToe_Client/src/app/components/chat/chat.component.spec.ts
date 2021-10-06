import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ChatService } from 'src/app/services/chatService/chat.service';
import { GameService } from 'src/app/services/gameService/game.service';
import { CookieModule } from 'ngx-cookie';
import { ChatComponent } from './chat.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ChatComponent', () => {
  let component: ChatComponent;
  let fixture: ComponentFixture<ChatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChatComponent ],
      imports:[CookieModule.forRoot(),RouterTestingModule,HttpClientTestingModule],
      providers:[ChatService,GameService]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
