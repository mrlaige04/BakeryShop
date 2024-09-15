import {inject, Inject, Injectable, signal} from '@angular/core';
import {ApiConfig} from "../config/apiConfig";
import {LoginModel} from "./models/login.model";
import {RegisterModel} from "./models/register.model";
import {HttpClient} from "@angular/common/http";
import {AccessTokenModel} from "./models/access-token.model";
import {UserModel} from "./models/user.model";
import {jwtDecode} from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TokenStorageKey = 'user-token'
  private readonly AdminRoleName = 'Admin'

  private _authToken = signal<AccessTokenModel | undefined>(undefined)
  public authToken = this._authToken.asReadonly();

  private _isAuthenticated = signal(this.initializeAuthentication());
  public isAuthenticated = this._isAuthenticated.asReadonly();

  private _isAdmin = signal(this.initializeAdminRulesIfExists());
  public isAdmin = this._isAdmin.asReadonly();

  private http = inject(HttpClient)

  private readonly baseAuthUrl: string;

  constructor(@Inject('API_CONFIG') private apiConfig: ApiConfig) {
    this.baseAuthUrl = apiConfig.apiUrl + '/users'
  }

  private initializeAuthentication(): boolean {
    const fromStorage = localStorage.getItem(this.TokenStorageKey)

    if (!fromStorage) {
      return false;
    }

    const parsed = <AccessTokenModel>JSON.parse(fromStorage)
    if (!parsed) {
      return false;
    }

    if (this.isTokenExpired(parsed))
    {
      return false;
    }

    this._authToken.set(parsed)

    return true;
  }

  private isTokenExpired(token: AccessTokenModel) {
    const expirationDate = new Date(token.expiresAt);
    return expirationDate <= new Date();
  }

  private initializeAdminRulesIfExists(): boolean {
    if (!this.isAuthenticated())
      return false;

    const token = this.authToken();
    if (!token)
      return false;

    return this.checkForAdmin(token);
  }


  register(model: RegisterModel) {
    const url = this.baseAuthUrl + '/register';
    return this.http.post<UserModel>(url, model)
  }

  login(model: LoginModel) {
    const url = this.baseAuthUrl + '/login';
    return this.http.post<AccessTokenModel>(url, model)
  }

  handleSuccessLogin(accessTokenModel: AccessTokenModel) {
    this._isAuthenticated.set(true)
    this._authToken.set(accessTokenModel)
    localStorage.setItem(this.TokenStorageKey, JSON.stringify(accessTokenModel))

    if (this.checkForAdmin(accessTokenModel)) {
     this._isAdmin.set(true)
    }
  }

  checkForAdmin(accessTokenModel: AccessTokenModel): boolean {
    const decoded = jwtDecode(accessTokenModel.accessToken);
    const roles = (decoded as any)['roles']

    return roles === this.AdminRoleName;
  }

  logout() {
    this._isAuthenticated.set(false);
    this._isAdmin.set(false);

    localStorage.removeItem(this.TokenStorageKey)
  }
}
