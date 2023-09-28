import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/app/core/snackbar-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    model: any = {};

    constructor(public accountService: AccountService, 
        public dialogRef: MatDialogRef<LoginComponent>,
        private router: Router,
        private snackBar: SnackbarService) {}

    ngOnInit(): void {
    }

    login() {
        this.accountService.login(this.model).subscribe({
            next: _ => {
                this.router.navigateByUrl('/');
                this.snackBar.getCustomSnackbar("Login success!")
            },
            error: err => {
                this.snackBar.getErrorSnackBar();                
            }
        });
        this.dialogRef.close();
    }

    onNoClick(): void {
        this.dialogRef.close();
      }
}
