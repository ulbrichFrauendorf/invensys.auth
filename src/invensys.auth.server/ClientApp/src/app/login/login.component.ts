import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    model: any = {};

    constructor(public accountService: AccountService, public dialogRef: MatDialogRef<LoginComponent>) {}

    ngOnInit(): void {
    }

    login() {       
        this.accountService.login(this.model).subscribe({
            next: res => console.log(res)
        });
        this.dialogRef.close();
    }

    onNoClick(): void {
        this.dialogRef.close();
      }
}
