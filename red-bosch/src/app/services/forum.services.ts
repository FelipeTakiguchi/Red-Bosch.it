import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ForumDTO } from 'src/DTO/ForumDTO';
import { ForumOwnerDTO } from 'src/DTO/ForumOwnerDTO';
import { UserForumDTO } from 'src/DTO/UserForumDTO';

@Injectable({
    providedIn: 'root',
})

export class ForumService {
    constructor(private http: HttpClient) { }

    register(formData: FormData) {
        return this.http.post(' http://localhost:5022/addForum', formData, {
            observe: 'response',
        });
    }

    getAll() {
        return this.http.get<ForumDTO[]>('http://localhost:5022/forums')
    }

    getForum(id: string) {
        return this.http.get<ForumOwnerDTO>('http://localhost:5022/forums/' + id)
    }

    addUserForum(formData: FormData){
        return this.http.post(' http://localhost:5022/enterForum', formData, {
            observe: 'response',
        });  
    }

    getSomeForum(id: string) {
        const headers = new HttpHeaders().set('id', id)
        return this.http.get<ForumDTO[]>('http://localhost:5022/getSomeForums', {headers})
    }
    
    getJoinedForums(id: string) {
        const headers = new HttpHeaders().set('id', id)
        return this.http.get<ForumDTO[]>('http://localhost:5022/getJoinedForums', {headers})
    }

    getAllUsersForum() {
        return this.http.get<UserForumDTO[]>('http://localhost:5022/getAllUsersForum/')
    }

    getUsersForum(id: string) {
        return this.http.get<UserForumDTO[]>('http://localhost:5022/getUsersForum/' + id)
    }

    exitForum(formData: FormData){
        return this.http.post(' http://localhost:5022/exitForum', formData, {
            observe: 'response',
        });  
    }

    deleteForum(formData: FormData){
        return this.http.post('http://localhost:5022/deleteForum', formData, {
            observe: 'response',
        });
    }
}