import {Component, inject, OnDestroy, OnInit, signal} from '@angular/core';
import {AuthService} from "../auth.service";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {LayoutService} from "../../layout/layout.service";
import {AccessTokenModel} from "../models/access-token.model";
import {Subscription} from "rxjs";
import {Router} from "@angular/router";
import {CardModule} from "primeng/card";
import {Button} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";

@Component({
  selector: 'bs-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CardModule,
    Button,
    InputTextModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit, OnDestroy {
  private auth = inject(AuthService)
  private fb = inject(FormBuilder)
  private layout = inject(LayoutService)

  private router = inject(Router)

  private loginSubscription: Subscription | undefined;

  errorMessage = signal<string | undefined>(undefined)

  ngOnInit() {
    this.layout.hideNavbar()
  }

  loginForm: FormGroup = this.fb.group({
    email: this.fb.control('', [Validators.required, Validators.email]),
    password: this.fb.control('', [
      Validators.required,
      Validators.minLength(6)
    ])
  })

  submit() {
    return;
    this.loginSubscription = this.auth.login(this.loginForm.value).subscribe({
      next: async (accessToken: AccessTokenModel) => {
        this.auth.handleSuccessLogin(accessToken)
        await this.router.navigate(['/'])
      }
    })
  }

  ngOnDestroy() {
    this.layout.showNavbar()
    this.loginSubscription?.unsubscribe()
  }
}
