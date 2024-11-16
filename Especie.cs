namespace ProjetoVendaEPagamento
{
    public class Especie : Pagamento
    {
        private double quantia;
        public double Quantia
        {
            get { return quantia; }
            set { quantia = value; }
        }
        private double troco;
        public double Troco
        {
            get { return troco; }
            set { troco = value; }
        }

        public Especie(double quantia, DateTime date, double total) : base(date, total)
        {
            Quantia = quantia;
            Troco = Quantia - Total;
        }
    }
}