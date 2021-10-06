import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoardComponent } from './components/board/board.component';
import { ChatComponent } from './components/chat/chat.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {path:'',component:LoginComponent},
  {path:'ChatPage',component:ChatComponent},
  {path:'Board',component:BoardComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
