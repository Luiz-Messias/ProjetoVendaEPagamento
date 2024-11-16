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
        private Pagamento? Pagamento { get; set; }

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
            int qtdTotal = 0;
            foreach (ItemVenda item in ItensVendas)
            {
                total += item.GetSubTotal();
                qtdTotal += item.Quantidade;
            }

            if (qtdTotal >= 50)
            {
                total *= 20 / 100;
            }

            return total;
        }
    }
}