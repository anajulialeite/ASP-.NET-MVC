using LanchesMac.Context;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _Context;

        public CarrinhoCompra(AppDbContext context)
        {
            _Context = context;
        }

        public string CarrinhoCompraID { get; set; }

        public List<CarrinhoCompraItem> carrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider service)
        {
            //Define uma seção
            ISession session =
                service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Obtendo um serviço do tipo do nosso contexto
            var context = service.GetService<AppDbContext>();

            //Obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //Atribui o ID do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //Retorna o caminho comm o texto e o ID atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraID = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _Context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraID);

            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraID,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _Context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _Context.SaveChanges();
        }
    }
}
