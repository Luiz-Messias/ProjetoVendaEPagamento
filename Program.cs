using ProjetoVendaEPagamento;

Console.Clear();

List<Produto> produtosDb = new();

void CadastrarProduto()
{
    Console.WriteLine("Cadastrando produto...");

    Console.Write("Nome: ");
    string nome = Console.ReadLine();

    Console.Write("Preço: ");
    double preco = double.Parse(Console.ReadLine());

    Console.Write("Estoque: ");
    int estoque = int.Parse(Console.ReadLine());

    Produto produto = new(nome, preco, estoque);

    produtosDb.Add(produto);

    Console.WriteLine("Produto cadastrado com sucesso!");
}

void ListarProdutos()
{
    Console.WriteLine("Listando produtos...");

    if (produtosDb.Count != 0)
        foreach (Produto produto in produtosDb)
        {
            Console.WriteLine($"Código: {produto.Codigo}");
            Console.WriteLine($"Nome: {produto.Nome}");
            Console.WriteLine($"Preço: {produto.Preco}");
            Console.WriteLine($"Estoque: {produto.Estoque}");
            Console.WriteLine();
        }
    else
        Console.WriteLine("Nenhum produto cadastrado!");
}

ItemVenda AdicionaritensNaVenda()
{
    Console.WriteLine("Adicionando itens na venda...");
    Console.Write("Digite o nome do produto: ");
    string nomeProduto = Console.ReadLine();

    Produto produto = produtosDb.Find(p => p.Nome.Equals(nomeProduto, StringComparison.OrdinalIgnoreCase));

    if (produto == null)
    {
        Console.WriteLine("Produto não encontrado!");
        return null;
    }

    Console.Write("Digite a quantidade: ");
    if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
    {
        Console.WriteLine("Quantidade inválida!");
        return null;
    }

    if (quantidade > produto.Estoque)
    {
        Console.WriteLine("Estoque insuficiente!");
        return null;
    }

    produto.Estoque -= quantidade;

    ItemVenda item = new(produto.Preco, quantidade, produto);

    Console.WriteLine($"Subtotal: R$ {item.Preco * item.Quantidade:F2}");
    return item;
}

void RealizarPagamento(Venda venda)
{
    Console.WriteLine("\nRealizando pagamento...");
    Console.WriteLine("Qual seria a forma de pagamento?");

    Console.Write("\n1 - Especie");
    Console.Write("\n2 - Cartão");
    Console.Write("\n3 - Cheque");
    Console.Write("\nOpção: ");
    int op = int.Parse(Console.ReadLine());

    switch (op)
    {
        case 1:
            Console.Write("Digite o valor da quantia: ");
            double quantia = double.Parse(Console.ReadLine());

            if (quantia < venda.Total)
            {
                Console.WriteLine("Valor insuficiente!");
                return;
            }

            Especie pagamentoEspecie = new(quantia, DateTime.Now, venda.Total);
            pagamentoEspecie.RealizarPagamento();
            Console.WriteLine($"Troco: R$ {pagamentoEspecie.Troco:F2}");
            break;
        //case 2:
        //    Console.Write("\nDigite o número do cartão: ");
        //    string numeroCartao = Console.ReadLine();
        //    Console.Write("\nDigite a data de validade: ");
        //    string dataValidade = Console.ReadLine();
        //    Console.Write("\nDigite o código de segurança: ");
        //    string codigoSeguranca = Console.ReadLine();

        //    Cartao pagamentoCartao = new("Dados corretos", 1, DateTime.Now, venda.Total);
        //    pagamentoCartao.RealizarPagamento();
        //    break;
        //case 3:
        //    Console.Write("Digite o número do cheque: ");
        //    int numeroCheque = Convert.ToInt32(Console.ReadLine());
        //    Console.Write("Digite o banco: ");
        //    string banco = Console.ReadLine();
        //    Console.Write("Digite a data de compensação: ");
        //    string dataCompensacao = Console.ReadLine();

        //    Cheque pagamentoCheque = new(numeroCheque, DateTime.Now, 0, DateTime.Now, venda.Total);
        //    pagamentoCheque.RealizarPagamento();
        //    break;
        default:
            Console.Write("Opção inválida.");
            break;
    }

}

void IniciarVenda()
{
    Console.WriteLine("Iniciando venda...");

    Venda venda = new();
    bool continuar = true;

    while (continuar)
    {
        var itemVenda = AdicionaritensNaVenda();

        if (itemVenda != null)
        {
            venda.AdicionarItem(itemVenda);
            Console.WriteLine("Item adicionado à venda com sucesso!");
        }
        else
            Console.WriteLine("Nenhum item adicionado.");

        Console.Write("Deseja adicionar mais itens na venda? (s/n): ");
        string resposta = Console.ReadLine().ToLower();

        if (resposta != "s")
            continuar = false;
    }

    venda.Total = venda.CalcularTotal();

    RealizarPagamento(venda);
}

int Menu()
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Iniciar venda");
    Console.WriteLine("2 - Cadastrar produtos");
    Console.WriteLine("3 - Listar produtos");
    Console.WriteLine("4 - Sair");
    Console.Write("Opção: ");
    int op = int.Parse(Console.ReadLine());
    return op;
}

int op = Menu();

while (op != 4)
{
    Console.Clear(); // Limpa o console
    switch (op)
    {
        case 1:
            IniciarVenda();
            break;
        case 2:
            CadastrarProduto();
            break;
        case 3:
            ListarProdutos();
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }

     Console.WriteLine("\nPressione qualquer tecla para continuar...");
     Console.ReadKey(); // Pausa antes de exibir o menu novamente
    op = Menu();
}