export interface PostDTO {
    id: number;
    imageId: number;
    conteudo: string;
    dataPublicacao: Date;
    idForum: number,
    jwt: string,
    participate?: boolean;
}