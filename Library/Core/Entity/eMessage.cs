namespace Almeida.Entity
{
    public class eMessage : eBase
    {
        public string Assunto { get; set; }
        public string Texto { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Sucesso = 0,
        Atenção = 1,
        Erro = 2
    } 
}
