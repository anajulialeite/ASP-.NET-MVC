using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [table("Categorias")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O nome do lanche deve ser informado.")]
        [Display(Name = "nome do Lanche")]
        [StringLength(80,MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínomo {1} e no máximo {2}.")]

        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada.")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Descrição deve exceder {1} caracteres.")]

        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada.")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Descrição deve exceder {1} caracteres.")]

        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "A descrição detalhada do lanche deve ser informada.")]
        [Display(Name = "Descrição detalhada do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada deve exceder {1} caracteres.")]

        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1,999.99,ErrorMessage = "O preço deve estar entre 1 e 999.99")]

        public string ImagemURL { get; set; }

        [Display(Name = "Caminho imagem normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres.")]

        public string ImagemThumbnaiURL { get; set; }

        [Display(Name = "Caminho imagem miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres.")]

        public bool IsLanchaPreferido { get; set; }

        [Display(Name = "Preferido?")]

        public bool EmEstoque { get; set; }

        [Display(Name = "Estoque")]

        //definindo os relacionamentos da tabela
        //definindo o relacionamento entre lanche e categoria
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
