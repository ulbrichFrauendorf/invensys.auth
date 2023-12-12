import { HttpClient, HttpContext } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'error-test',
  templateUrl: './error-test.component.html',
  styleUrls: ['./error-test.component.scss'],
})
export class ErrorTestComponent {
  private baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  get500() {
    this.http.get(this.baseUrl + 'bug/bad-request').subscribe();
  }
}
