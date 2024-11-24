namespace ProjetoVendaEPagamento
{
    public class Venda
    {
        private DateTime data;
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        private List<ItemVenda> ItensVendas { get; set; }

        private Pagamento? pagamento;
        public Pagamento? Pagamento
        {
            get { return pagamento; }
            set { pagamento = value; }
        }

        public Venda()
        {
            Data = DateTime.Now;
            ItensVendas = new List<ItemVenda>();
        }

        public void AdicionarItem(ItemVenda item)
        {
            ItensVendas.Add(item);
            Total += item.GetSubTotal();
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (var item in ItensVendas)
            {
                double desconto = item.Quantidade >= 50 ? 0.2 : 0;
                total += item.SubTotal * (1 - desconto);
            }

            return total;
        }

        public void MostrarAtributosDaVenda()
        {
            foreach (var item in ItensVendas)
            {
                item.MostrarAtributos();
            }

            Console.WriteLine($"\nTotal: R$ {Total:F2}");
        }
    }
}