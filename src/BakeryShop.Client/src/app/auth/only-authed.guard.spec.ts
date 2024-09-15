import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { onlyAuthedGuard } from './only-authed.guard';

describe('onlyAuthedGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => onlyAuthedGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
