namespace ProjetoVendaEPagamento
{
    public abstract class Pagamento
    {
        protected DateTime Date { get; set; } = new DateTime();
        protected double Total { get; set; }

        public Pagamento(DateTime date, double total)
        {
            Date = date;
            Total = total;
        }

        public void RealizarPagamento()
        {
            Console.WriteLine($"\nPagamento realizado em {Date.ToShortDateString()} com o total de R$ {Total:C2}");
            Console.WriteLine("Pagamento efetuado com sucesso!");
        }
    }
}