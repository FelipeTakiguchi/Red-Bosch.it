export interface CommentDTO {
    id: number;
    conteudo: string;
    usuario: string;
    dataPublicacao: Date;
    idPost: number;
}