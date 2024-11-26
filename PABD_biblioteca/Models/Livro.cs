namespace PABD_biblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int  QuantidadePaginas{ get; set; }
        public string Descricao { get; set; }
        public DateTime AnoPublicacao { get; set; }

    }
}
