import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    users:any =null;

    constructor(private httpClient: HttpClient){}
    ngOnInit(): void {
       this.getUsers();
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
