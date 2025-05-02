

namespace LanchesMac.Repositories.Interfaces
{
    internal class _context
    {
        public static object Lanches { get; internal set; }
        public static IEnumerable<object> CarrinhoCompraItens { get; internal set; }

        internal static void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}