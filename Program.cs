using ProjetoVendaEPagamento;

Console.Clear();

List<Produto> produtosDb = new();

void CadastrarProduto()
{
    Console.Write("Deseja cadastrar manualmente ou automático? (m/a)");
    Console.Write("\nOpção: ");
    string opcao = Console.ReadLine().ToLower();

    if (opcao == "m")
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
    else if (opcao == "a")
    {
        // Instancia uma lista de produtos com alguns produtos

        Console.Write("Produtos cadastro de forma automática");
        produtosDb.AddRange(new List<Produto>
        {
            new("Notebook Dell", 3500, 10),
            new("Smartphone Samsung", 2200, 15),
            new("Fone de Ouvido JBL", 299, 50),
            new("Mouse Logitech", 120, 30),
            new("Teclado Mecânico Redragon", 450, 20),
            new("Monitor LG 24''", 1200, 12),
            new("Cadeira Gamer DXRacer", 890, 8),
            new("Webcam Full HD Logitech", 329, 25),
            new("Impressora Multifuncional HP", 750, 5),
            new("SSD Kingston 500GB", 399, 18)
         });
    }
    else
        Console.WriteLine("Opção inválida!");
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

    Produto produto = produtosDb.Find(p => p.Nome.ToLower() == nomeProduto.ToLower());

    if (produto == null)
    {
        Console.WriteLine("Produto não encontrado!");
        return null;
    }

    Console.Write("Digite a quantidade: ");
    int quantidade = Convert.ToInt32(Console.ReadLine());

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

            venda.Pagamento = pagamentoEspecie;
            Console.WriteLine($"Troco: R$ {pagamentoEspecie.Troco:F2}");

            venda.MostrarAtributosDaVenda();
            break;
        case 2:
            Console.Write("\nDigite o número do cartão: ");
            string numeroCartao = Console.ReadLine();

            Cartao pagamentoCartao = new(numeroCartao, 1, DateTime.Now, venda.Total);
            venda.Pagamento = pagamentoCartao;
            pagamentoCartao.RealizarPagamento();
            venda.MostrarAtributosDaVenda();
            break;
        case 3:
            Console.Write("Digite o número do cheque: ");
            int numeroCheque = Convert.ToInt32(Console.ReadLine());

            Cheque pagamentoCheque = new(numeroCheque, DateTime.Now, 0, DateTime.Now, venda.Total);
            venda.Pagamento = pagamentoCheque;

            pagamentoCheque.RealizarPagamento();
            venda.MostrarAtributosDaVenda();
            break;
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
    int op = Convert.ToInt32(Console.ReadLine());
    return op;
}

int op = Menu();

while (op != 4)
{
    Console.Clear();
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
    Console.ReadKey();
    op = Menu();
}