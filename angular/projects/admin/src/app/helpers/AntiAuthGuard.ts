import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from 'projects/shared/src/lib/services/api/authentication/authentication.service';

@Injectable({ providedIn: 'root' })
export class AntiAuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.authenticationService.currentUserValue;
        if (!currentUser) {
            // not logged in so return true
            return true;
        }

        this.router.navigate(['/']);
        return false;
    }
}
