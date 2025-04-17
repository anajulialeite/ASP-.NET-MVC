using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models
{
    [table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Required(ErrorMessage = "Informe o nome da categoria.")]
        [Display(Name = "nome")]
        
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho máximo é 200 caracteres.")]
        [Required(ErrorMessage = "Informe a descrição da categoria.")]
        [Display(Name = "nome")]
        
        public string Descricao { get; set; }

        //definindo uma gategoria de um para muitos
        public List<Lanche> Lanches { get; set; }
    }
}
