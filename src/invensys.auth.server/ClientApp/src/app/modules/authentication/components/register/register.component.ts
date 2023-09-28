import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
    model: any = {};
    @Output() cancelRegister = new EventEmitter();

    constructor(private accountService: AccountService) { }

    ngOnInit(): void {

    }

    register(): void {
        this.accountService.register(this.model).subscribe({
            next: () => {
                this.cancel();
            },
            error: err => {
                console.error(err.error);
            }
        });
    }

    cancel(): void {
        this.cancelRegister.emit(false);
    }
}
