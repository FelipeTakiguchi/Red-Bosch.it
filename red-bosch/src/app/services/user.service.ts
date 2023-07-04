import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from 'src/DTO/LoginDTO';
import { UserToken } from 'src/DTO/Token';
import { Jwt } from 'src/DTO/Jwt';
import { UserDTO } from 'src/DTO/UserDTO';

@Injectable({
    providedIn: 'root',
})

export class UserService {
    constructor(private http: HttpClient) { }

    login(loginData: LoginDTO) {
        return this.http.post('http://localhost:5022/login', loginData, {
            observe: 'response',
        });
    }

    register(formData: FormData) {
        return this.http.post(' http://localhost:5022/register', formData, {
            observe: 'response',
        });
    }

    validateJwt(jwt: Jwt) {
        return this.http.post<UserToken>('http://localhost:5022/validate', jwt, { 
            observe: 'response',
        })
    }

    getUser(jwt: Jwt) {
        return this.http.post<UserDTO>('http://localhost:5022/getUser', jwt, { 
            observe: 'response',
        })
    }
}