using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("CarrinhoCompraItens")]
    public class CarrinhoCompraItem
    {
    
            [Key]
            public int CarrinhoCompraItemId { get; set; }

            // Chave estrangeira para Lanche
            public int LancheId { get; set; } // Corrigido o nome da propriedade para "LancheId"

            // Propriedade de navegação
            public Lanche Lanche { get; set; }

            public int Quantidade { get; set; }

            // Propriedade obrigatória, garantir que sempre tenha valor
            [StringLength(200)]
            public string CarrinhoCompraId { get; set; }
        
    }
}

