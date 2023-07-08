import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { VoteDTO } from 'src/DTO/voteDTO';

@Injectable({
    providedIn: 'root',
})

export class VoteService {
    constructor(private http: HttpClient) { }

    addVote(formData: FormData) {
        return this.http.post(' http://localhost:5022/addVote', formData, {
            observe: 'response',
        });
    }

    getVote(idPost: string) {
        return this.http.get<VoteDTO[]>('http://localhost:5022/votes/' + idPost)
    }
}