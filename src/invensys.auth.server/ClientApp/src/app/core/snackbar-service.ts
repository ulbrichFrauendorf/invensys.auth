import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
@Injectable({
    providedIn: 'root'
})

export class SnackbarService {
    private duration = 3000;

    horizontalPosition: MatSnackBarHorizontalPosition = 'center';
    verticalPosition: MatSnackBarVerticalPosition = 'top';

    constructor(private snackBar: MatSnackBar) {
    }

    public getErrorSnackBar(): void {
        this.snackBar.open("Something went wrong", "Dismiss", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
            duration: this.duration
        });
    }

    public getCustomSnackbar(customText: string) {
        this.snackBar.open(customText, "Dismiss", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
            duration: this.duration
        });
    }

    public getCustomSnackbarWithItemName(customText: string, itemName: string) {
        this.snackBar.open(`${customText} (${itemName})`, "Dismiss", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
            duration: this.duration
        });
    }
}
