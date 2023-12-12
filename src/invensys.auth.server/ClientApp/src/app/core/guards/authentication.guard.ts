import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthUser } from 'src/app/modules/authentication/models/user';
import { AccountService } from 'src/app/modules/authentication/services/account.service';
import { SnackbarService } from '../snackbar-service';

export const authenticationGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const snackBarService = inject(SnackbarService);
  const router = inject(Router);

  return accountService.currentAuthUser$.pipe(
    map((user)=>{
      if(user){
        return true;
      } 
      else{
        console.log(false);
        snackBarService.getErrorSnackBar();
        return false;
      }
    })
  )
};
