import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ForumDTO } from 'src/DTO/ForumDTO';

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
        return this.http.get<ForumDTO>('http://localhost:5022/forums/' + id)
    }
}