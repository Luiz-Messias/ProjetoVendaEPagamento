namespace ProjetoVendaEPagamento
{
    public class ItemVenda
    {
        private double preco;
        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }
        private int quantidade;
        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
        private double subTotal;
        public double SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }

        private Produto Produto { get; set; }

        public ItemVenda(double preco, int quantidade, Produto produto)
        {
            Preco = preco;
            Quantidade = quantidade;
            Produto = produto;
            SubTotal = Preco * Quantidade;
        }

        public double GetSubTotal()
        {
            return SubTotal;
        }

        public void MostrarAtributos()
        {
            Console.WriteLine($"Produto: {Produto.Nome}");
            Console.WriteLine($"Preço: R$ {Preco:F2}");
            Console.WriteLine($"Quantidade: {Quantidade}");
            Console.WriteLine($"Subtotal: R$ {SubTotal:F2}");
        }
    }
}