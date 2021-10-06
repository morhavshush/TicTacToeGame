import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CookieModule } from 'ngx-cookie';
import { ChatService } from 'src/app/services/chatService/chat.service';
import { LoginComponent } from './login.component';
import {RouterTestingModule} from'@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let chatServiceStub: Partial<ChatService>;

  beforeEach(async () => {


    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports:[CookieModule.forRoot(), RouterTestingModule, HttpClientTestingModule],
      providers: [ { provide: ChatService, useValue:chatServiceStub } ],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});





// describe('AppComponent', () => {
//   let cookieServiceMock: jasmine.SpyObj<CookieService>;
//   let fixture: ComponentFixture<AppComponent>;

//   beforeEach(() => {
//       cookieServiceMock = jasmine.createSpyObj<CookieService>('CookieService', ['check', 'set', 'delete']);
//       cookieServiceMock.check.and.returnValue(true);

//       TestBed.configureTestingModule({
//           declarations: [
//               AppComponent
//           ],
//       });

//       fixture = TestBed.overrideComponent(AppComponent, {
//           set: {
//               providers: [{
//                   provide: CookieService,
//                   useValue: cookieServiceMock
//               }]
//           }
//       }).createComponent(AppComponent);
//   });

//   it('should mock cookieService', () => {
//       const app = fixture.debugElement.componentInstance;

//       expect(app.cookieService.check('foobar')).toBe(true);
//       expect(app.cookieService.check).toHaveBeenCalledWith('foobar');
//   });
// });
