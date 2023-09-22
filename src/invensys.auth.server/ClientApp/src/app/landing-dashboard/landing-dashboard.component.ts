import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing-dashboard',
  templateUrl: './landing-dashboard.component.html',
  styleUrls: ['./landing-dashboard.component.scss']
})
export class LandingDashboardComponent implements OnInit {
    registerMode = true;
    users:any =null;

    constructor(private httpClient: HttpClient){}
    ngOnInit(): void {
       this.getUsers();
    }

    toggleRegisterMode(){
        this.registerMode = !this.registerMode;
    }

    cancelRegisterMode(event:boolean){
        this.registerMode = event;
    }
    
    getUsers(){
        this.httpClient.get('https://localhost:5001/api/authusers').subscribe(
            {
                next: response => this.users = response,
                error: error => console.log(error),
                complete: () => console.log('Completed request'),
            }
        )
    }
}
