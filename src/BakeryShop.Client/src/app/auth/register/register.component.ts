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
import {AccessTokenModel} from "../models/access-token.model";
import {UserModel} from "../models/user.model";

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
        RouterLink
    ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit, OnDestroy{
  private auth = inject(AuthService)
  private fb = inject(FormBuilder)
  private layout = inject(LayoutService)

  private router = inject(Router)

  private registerSubscription: Subscription | undefined;

  errorMessage = signal<string | undefined>(undefined)

  ngOnInit() {
    this.layout.hideNavbar()
  }

  registerForm: FormGroup = this.fb.group({
    email: this.fb.control('', [Validators.required, Validators.email]),
    password: this.fb.control('', [
      Validators.required,
      Validators.minLength(6)
    ])
  })

  submit() {
    this.registerSubscription = this.auth.register(this.registerForm.value).subscribe({
      next: async (user: UserModel) => {
        console.log(user)
        await this.router.navigate(['/'])
      }
    })
  }

  ngOnDestroy() {
    this.layout.showNavbar()
    this.registerSubscription?.unsubscribe()
  }
}
