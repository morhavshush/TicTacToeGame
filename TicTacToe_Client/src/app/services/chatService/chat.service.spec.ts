import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ChatService } from './chat.service';
import { CookieModule } from 'ngx-cookie';


describe('ChatService', () => {
    let service: ChatService;

       beforeEach(() => {
         TestBed.configureTestingModule({
           imports:[HttpClientTestingModule,CookieModule.forRoot()]
         });
         service = TestBed.inject(ChatService);
       })

       
       it('should be created', () => {
         expect(service).toBeTruthy();
       });
});
