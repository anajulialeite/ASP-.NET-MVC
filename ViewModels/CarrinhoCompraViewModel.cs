using LanchesMac.Context;
using LanchesMac.Models;

namespace LanchesMac.ViewModels
{
    public class CarrinhoCompraViewModel
    {
        public CarrinhoCompra CarrinhoCompra { get; set; } = default!;

        public decimal CarrinhoCompraTotal { get; set; }
    }
}

