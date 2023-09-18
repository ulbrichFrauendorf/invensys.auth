import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {

    title = 'Invensys Authentication Server';
    users: any;

    constructor(private httpClient: HttpClient) {

    }

    ngOnInit(): void {
        this.httpClient.get('https://localhost:5001/api/authuser').subscribe(
            {
                next: response => this.users = response,
                error: error => console.log(error),
                complete: () => console.log('Completed request'),
            }
        )
    }
}
