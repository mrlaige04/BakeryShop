import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AuthService} from "./auth.service";

export const onlyAuthedGuard: CanActivateFn = async (route, state) => {
  const auth = inject(AuthService)
  const router = inject(Router)

  if (auth.isAuthenticated()) {
    return true;
  }

  auth.logout()
  await router.navigate(['/auth/login']);
  return false;
};
