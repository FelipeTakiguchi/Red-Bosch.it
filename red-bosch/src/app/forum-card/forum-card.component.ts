import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';
import { PostService } from '../services/post.service';
import { Router } from '@angular/router';
import { VoteService } from '../services/vote.service';
import { VoteDTO } from 'src/DTO/voteDTO';
import { CommentService } from '../services/comment.service';
import { CommentDTO } from 'src/DTO/CommentDTO';

@Component({
  selector: 'app-forum-card',
  templateUrl: './forum-card.component.html',
  styleUrls: ['./forum-card.component.css']
})
export class ForumCardComponent implements OnInit {
  @Input() id: number = 0;
  @Input() conteudo: string = '';
  @Input() dataPublicacao?: Date;
  @Input() idUsuario: string = '';
  @Input() imageUsuario: string = '';
  @Input() nomeUsuario: string = '';
  @Input() imagemId: number = 0;
  @Input() isLogged: boolean = false;
  @Input() idForum: string = "";
  @Input() isDelete: boolean = false;
  @Input() votesQuantity: number = 0;
  @Input() commentText: string = "";
  @Input() comments: CommentDTO[] = [];
  @Input() participate: boolean = false;
  @Output() onPost = new EventEmitter<any>();

  formData: FormData = new FormData();

  protected Upvoted = false;
  protected Downvoted = false;

  constructor(private userService: UserService, private postService: PostService,
    private router: Router, private voteService: VoteService, private commentService: CommentService) { }

  jwt: Jwt = {
    jwt: '',
  }

  ngOnInit(): void {
    if (sessionStorage.getItem("jwtSession") != null) {
      this.jwt.jwt = sessionStorage.getItem("jwtSession")!
      this.isLogged = true;
    }
    else
      this.isLogged = false;

    if (this.jwt.jwt == this.idUsuario)
      this.isDelete = true;

    this.jwt.jwt = this.idUsuario;
    this.userService.getUser(this.jwt)
      .subscribe(res => {
        if (res.status == 200) {
          if (res.body?.nome != undefined)
            this.nomeUsuario = res.body.nome;
          if (res.body?.imageId.toString() != undefined)
            this.imageUsuario = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })

    this.voteService.getVote(this.id.toString())
      .subscribe(res => {
        this.getVotes()
      })
      
    this.commentService.getComments(this.id.toString())
      .subscribe(list => {
        var newList: CommentDTO[] = []
        list.forEach(comment => {
          newList.push({
            id: comment.id,
            conteudo: comment.conteudo,
            dataPublicacao: comment.dataPublicacao,
            idPost: comment.idPost,
            usuario: comment.usuario
          })
        });

        this.comments = newList
      })
  }

  protected changeDownvote() {
    this.formData.delete("idUsuario")
    this.formData.delete("idPost")
    this.formData.delete("state")
    this.formData.append("idUsuario", sessionStorage.getItem("jwtSession")!)
    this.formData.append("idPost", this.id.toString())
    this.formData.append("state", "false")

    this.voteService.addVote(this.formData)
      .subscribe(res => {
        if (res.status == 200) {
          this.getVotes()
        }
      })

    this.Upvoted = false;
    this.Downvoted = true;
    this.onPost.emit()
  }

  protected changeUpvote() {
    this.formData.delete("idUsuario")
    this.formData.delete("idPost")
    this.formData.delete("state")
    this.formData.append("idUsuario", sessionStorage.getItem("jwtSession")!)
    this.formData.append("idPost", this.id.toString())
    this.formData.append("state", "true")

    this.voteService.addVote(this.formData)
      .subscribe(res => {
        if (res.status == 200) {
          this.getVotes()
        }
      })

    this.Downvoted = false;
    this.Upvoted = true;
    this.onPost.emit()
  }

  deletePost() {
    this.formData.set("idPost", this.id.toString())
    this.postService.deletePost(this.formData)
      .subscribe(res => {
        window.location.reload()
      })
  }

  getVotes() {
    this.voteService.getVote(this.id.toString())
      .subscribe(list => {
        var newList: VoteDTO[] = []
        list.forEach(vote => {
          if (vote.state) {
            newList.push({
              id: vote.id,
              idUsuario: vote.idUsuario,
              idPost: vote.idPost,
              state: vote.state
            })
          }
          if (vote.idUsuario == sessionStorage.getItem("jwtSession"))
            if (vote.state) {
              this.Upvoted = true
              this.Downvoted = false
            }
            else {
              this.Downvoted = true
              this.Upvoted = false
            }
        });

        this.votesQuantity = newList.length
      })

  }

  createComment() {
    this.formData.delete("idUsuario")
    this.formData.delete("idPost")
    this.formData.delete("content")
    this.formData.delete("date")
    this.formData.append("idUsuario", sessionStorage.getItem("jwtSession")!)
    this.formData.append("idPost", this.id.toString())
    this.formData.append("date", new Date().toLocaleDateString())
    this.formData.append("conteudo", this.commentText)

    this.commentService.addComment(this.formData)
      .subscribe(res => {
        if (res.status == 200) {
          this.commentService.getComments(this.id.toString())
            .subscribe(list => {
              var newList: CommentDTO[] = []
              list.forEach(comment => {
                newList.push({
                  id: comment.id,
                  conteudo: comment.conteudo,
                  dataPublicacao: comment.dataPublicacao,
                  idPost: comment.idPost,
                  usuario: comment.usuario
                })
              });

              this.comments = newList
            })
        }
      })
  }
}
