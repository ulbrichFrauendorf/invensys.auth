import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    model: any = {};
    userIsLoggedIn = false;

    constructor(private accountService: AccountService) {}

    ngOnInit(): void {
        
    }

    login() {
        console.log(this.model);
        
        this.accountService.login(this.model).subscribe({
            next: response => {
                console.log(response);
                this.userIsLoggedIn = true;
            },
            error: error => console.log(error)            
        })
    }
}
