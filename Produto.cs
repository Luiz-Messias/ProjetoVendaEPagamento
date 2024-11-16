namespace ProjetoVendaEPagamento
{
    public class Produto
    {
        private static int QuantidadeProduto;
        private long codigo;
        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private double preco;
        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }
        private int estoque;
        public int Estoque
        {
            get { return estoque; }
            set { estoque = value; }
        }

        public Produto(string nome, double preco, int estoque)
        {
            Codigo = QuantidadeProduto++; // Auto incremento
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
        }
    }
}