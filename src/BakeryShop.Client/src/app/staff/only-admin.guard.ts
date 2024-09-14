import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AuthService} from "../auth/auth.service";

export const onlyAdminGuard: CanActivateFn = async (route, state) => {
  const auth = inject(AuthService)
  const router = inject(Router);

  if (!auth.isAuthenticated() || !auth.isAdmin()) {
    await router.navigate(['/login']);
    return false;
  }

  return true;
};
