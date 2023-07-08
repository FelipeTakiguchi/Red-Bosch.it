import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PostDTO } from 'src/DTO/PostDTO';

@Injectable({
    providedIn: 'root',
})

export class PostService {
    constructor(private http: HttpClient) { }

    addPost(formData: FormData) {
        return this.http.post(' http://localhost:5022/addPost', formData, {
            observe: 'response',
        });
    }

    getPosts(forumId: string) {
        return this.http.get<PostDTO[]>('http://localhost:5022/posts/' + forumId)
    }
    
    getAll() {
        return this.http.get<PostDTO[]>('http://localhost:5022/posts')
    }

    deletePost(formData: FormData) {
        return this.http.post<PostDTO>('http://localhost:5022/deletePost/', formData, {
            observe: 'response',
        })
    }
}