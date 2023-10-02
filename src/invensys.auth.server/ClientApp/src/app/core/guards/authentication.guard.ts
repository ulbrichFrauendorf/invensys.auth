import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { map } from 'rxjs';
import { AuthUser } from 'src/app/modules/authentication/models/user';
import { AccountService } from 'src/app/modules/authentication/services/account.service';
import { SnackbarService } from '../snackbar-service';

export const authenticationGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const snackBarService = inject(SnackbarService);

  return accountService.currentAuthUser$.pipe(
    map((user)=>{
      console.log(user);
      
      if(user){
        console.log(true);
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
