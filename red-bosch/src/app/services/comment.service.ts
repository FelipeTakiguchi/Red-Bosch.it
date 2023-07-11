import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommentDTO } from 'src/DTO/CommentDTO';

@Injectable({
    providedIn: 'root',
})

export class CommentService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<CommentDTO[]>('http://localhost:5022/comments')
    }

    getComments(id: string) {
        return this.http.get<CommentDTO[]>('http://localhost:5022/comments/' + id)
    }

    addComment(formData: FormData) {
        return this.http.post(' http://localhost:5022/addComment', formData, {
            observe: 'response',
        });
    }

    deleteComment(formData: FormData) {
        return this.http.post(' http://localhost:5022/deleteComment', formData, {
            observe: 'response',
        });
    }
}