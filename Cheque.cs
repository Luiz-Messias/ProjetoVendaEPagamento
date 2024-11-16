namespace ProjetoVendaEPagamento
{
    public class Cheque : Pagamento
    {
        private long numero;
        public long Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        private DateTime dataDeposito;
        public DateTime DataDeposito
        {
            get { return dataDeposito; }
            set { dataDeposito = value; }
        }
        private int situacao;
        public int Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        public Cheque(long numero, DateTime dataDeposito, int situacao, DateTime date, double total) : base(date, total)
        {
            Numero = numero;
            DataDeposito = dataDeposito;
            Situacao = situacao;
        }
    }
}