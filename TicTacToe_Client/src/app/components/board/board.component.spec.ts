import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GameService } from 'src/app/services/gameService/game.service';
import { CookieModule } from 'ngx-cookie';
import { BoardComponent } from './board.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('BoardComponent', () => {
  let component: BoardComponent;
  let fixture: ComponentFixture<BoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BoardComponent ],
      imports:[HttpClientTestingModule,CookieModule.forRoot(),RouterTestingModule],
      providers: [GameService]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
