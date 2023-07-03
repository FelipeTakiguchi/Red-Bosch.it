export interface RegisterDTO {
    Nome: string;
    Senha: string;
    Email: string;
    DataNascimento: Date;
    Foto?: File;
    FotoNome?: string;
}