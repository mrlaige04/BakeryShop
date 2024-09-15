import {Component, inject, OnDestroy, OnInit, signal} from '@angular/core';
import {Button} from "primeng/button";
import {CardModule} from "primeng/card";
import {InputTextModule} from "primeng/inputtext";
import {PaginatorModule} from "primeng/paginator";
import {PrimeTemplate} from "primeng/api";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {AuthService} from "../auth.service";
import {LayoutService} from "../../layout/layout.service";
import {Subscription} from "rxjs";
import {UserModel} from "../models/user.model";
import {JsonPipe} from "@angular/common";

@Component({
  selector: 'bs-register',
  standalone: true,
  imports: [
    Button,
    CardModule,
    InputTextModule,
    PaginatorModule,
    PrimeTemplate,
    ReactiveFormsModule,
    RouterLink,
    JsonPipe
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit, OnDestroy{
  private auth = inject(AuthService)
  private fb = inject(FormBuilder)
  private layout = inject(LayoutService)

  private router = inject(Router)

  triedToSubmit = false;

  private registerSubscription: Subscription | undefined;

  errorMessage = signal<string | undefined>(undefined)

  ngOnInit() {
    this.layout.hideNavbar()
  }

  registerForm: FormGroup = this.fb.group({
    email: this.fb.control('', [Validators.required, Validators.email]),
    password: this.fb.control('', [
      Validators.required,
      Validators.minLength(5)
    ])
  })

  submit() {
    this.triedToSubmit = true;
    if (!this.registerForm.valid) {
      return;
    }
    this.registerSubscription = this.auth.register(this.registerForm.value)
      .subscribe({
        next: async (user: UserModel) => {
          console.log(user)
          await this.router.navigate(['/'])
        },
        error: err => console.log(err.error.detail)
      })
  }

  ngOnDestroy() {
    this.layout.showNavbar()
    this.registerSubscription?.unsubscribe()
  }
}
