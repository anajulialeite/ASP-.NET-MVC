using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models
{
    [table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O nome do lanche deve ser informado.")]
        [Display(Name = "nome do Lanche")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínomo {1} e no máximo {2}.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada.")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Descrição deve exceder {1} caracteres.")]
        public string? DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada.")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Descrição deve exceder {1} caracteres.")]
        public string? DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1 e 999.99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho imagem normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres.")]
        public string? ImagemURL { get; set; }

        [Display(Name = "Caminho imagem miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres.")]
        public string? ImagemThumbnailURL { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }


        //definindo os relacionamentos da tabela
        //definindo o relacionamento entre lanche e categoria
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
