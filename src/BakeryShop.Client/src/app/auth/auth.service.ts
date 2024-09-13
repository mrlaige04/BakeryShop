import {inject, Inject, Injectable, signal} from '@angular/core';
import {ApiConfig} from "../config/apiConfig";
import {LoginModel} from "./models/login.model";
import {RegisterModel} from "./models/register.model";
import {HttpClient} from "@angular/common/http";
import {AccessTokenModel} from "./models/access-token.model";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _isAuthenticated = signal(this.initializeAuthentication());
  public isAuthenticated = this._isAuthenticated.asReadonly();

  private http = inject(HttpClient)

  private readonly baseAuthUrl: string;

  constructor(@Inject('API_CONFIG') private apiConfig: ApiConfig) {
    this.baseAuthUrl = apiConfig.apiUrl + '/users'
  }

  private initializeAuthentication(): boolean {
    return false;
  }


  register(model: RegisterModel) {
    const url = this.baseAuthUrl + '/register';
    return this.http.post(url, model)
  }

  login(model: LoginModel) {
    const url = this.baseAuthUrl + '/login';
    return this.http.post<AccessTokenModel>(url, model)
  }

  logout() {
    this._isAuthenticated.set(false);
  }

  handleSuccessLogin(accessTokenModel: AccessTokenModel) {
    this._isAuthenticated.set(true)
  }
}
