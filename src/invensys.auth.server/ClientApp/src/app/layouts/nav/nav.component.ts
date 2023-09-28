import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout'
import { MatSidenav } from '@angular/material/sidenav';
import { AccountService } from '../../modules/authentication/services/account.service';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../../modules/authentication/components/login/login.component';
import { SnackbarService } from 'src/app/core/snackbar-service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements AfterViewInit {

    @ViewChild(MatSidenav) sidenav!: MatSidenav;

    constructor(private observer: BreakpointObserver, 
        private snackbarService: SnackbarService,
        public accountService: AccountService, public dialog: MatDialog) {}


    ngAfterViewInit(): void {
        this.observer.observe(["(max-width: 800px)"]).subscribe((res) => {
          if (res.matches) {
            this.sidenav.mode = "over";
            this.sidenav.close();
          } else {
            this.sidenav.mode = "side";
            this.sidenav.open();
          }
        });
      }

      login() {
        const dialogRef = this.dialog.open(LoginComponent);
    }

    logout(){
        this.accountService.logout();
        this.snackbarService.getCustomSnackbar("Logout success!")
    }
}
