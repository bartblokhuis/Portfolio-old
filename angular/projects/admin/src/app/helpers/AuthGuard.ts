import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from 'projects/shared/src/lib/services/api/authentication/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private authenticationService: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.authenticationService.currentUserValue;
        if (currentUser) {
            const userSignedInTill = new Date(currentUser.expiration);
            const userSignedInTillUTC = new Date(userSignedInTill.getUTCFullYear(),
                userSignedInTill.getUTCMonth(),
                userSignedInTill.getUTCDate(),
                userSignedInTill.getUTCHours(),
                userSignedInTill.getUTCMinutes(),
                userSignedInTill.getUTCSeconds());

            var currentDate = new Date();
            const currentDateUTC = new Date(currentDate.getUTCFullYear(),
                currentDate.getUTCMonth(),
                currentDate.getUTCDate(),
                currentDate.getUTCHours(),
                currentDate.getUTCMinutes(),
                currentDate.getUTCSeconds());

            if(userSignedInTillUTC > currentDateUTC) {
                return true;
            }
            
            //User session is expired
            this.authenticationService.logout();
        }

        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
