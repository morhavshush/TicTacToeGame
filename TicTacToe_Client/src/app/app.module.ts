import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/appComponent/app.component';
import { ChatComponent } from './components/chat/chat.component';
import { HttpClientModule } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/login/login.component';
import { CookieModule } from 'ngx-cookie';
import { MatCardModule } from '@angular/material/card';
import { BoardComponent } from './components/board/board.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    LoginComponent,
    BoardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatFormFieldModule,
    HttpClientModule,
    MatIconModule,
    FormsModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    MatProgressSpinnerModule,
    CookieModule.forRoot()
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
