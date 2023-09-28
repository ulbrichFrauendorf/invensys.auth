import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './modules/authentication/services/account.service';
import { AuthUser } from './modules/authentication/models/user';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {

    title = 'Invensys Authentication Server';
   
    constructor(private httpClient: HttpClient, private accountService: AccountService) {

    }

    ngOnInit(): void {
        this.setCurrentUser();
    }

    setCurrentUser(){
        const userString = localStorage.getItem('user');
        if(!userString) return;

        const user: AuthUser = JSON.parse(userString);
        this.accountService.setCurrentUser(user);
    }
}
