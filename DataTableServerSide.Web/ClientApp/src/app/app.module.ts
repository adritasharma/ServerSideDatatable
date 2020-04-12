import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { UserListComponent } from './user-list/user-list.component';
import { HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { DataTablesModule } from 'angular-datatables';

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      UserListComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      DataTablesModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
