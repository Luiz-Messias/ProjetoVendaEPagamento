namespace ProjetoVendaEPagamento
{
    public class Cartao : Pagamento
    {
        private string dadosTransacao;
        public string DadosTransacao
        {
            get { return dadosTransacao; }
            set { dadosTransacao = value; }
        }
        private int resultadoTransacao;
        public int ResultadoTransacao
        {
            get { return resultadoTransacao; }
            set { resultadoTransacao = value; }
        }

        public Cartao(string dadosTransacao, int resultadoTransacao, DateTime date, double total)
        : base(date, total)
        {
            DadosTransacao = dadosTransacao;
            ResultadoTransacao = resultadoTransacao;
        }
    }
}