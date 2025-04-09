namespace LanchesMac.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        public string CategoriaNome { get; set; }

        public string Descricao { get; set; }

        //definindo uma gategoria de um para muitos
        public List<Lanche> Lanches { get; set; }
    }
}
