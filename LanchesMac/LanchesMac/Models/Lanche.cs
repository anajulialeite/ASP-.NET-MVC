namespace LanchesMac.Models
{
    public class Lanche
    {
        public int LancheId { get; set; }

        public string Nome { get; set; }

        public string DescricaoCurta { get; set; }

        public string DescricaoDetalhada { get; set; }

        public decimal Preco { get; set; }

        public string ImagemURL { get; set; }

        public string ImagemThumbnaiURL { get; set; }

        public bool IsLanchaPreferido { get; set; }

        public bool EmEstoque { get; set; }

        //definindo os relacionamentos da tabela
        //definindo o relacionamento entre lanche e categoria
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
