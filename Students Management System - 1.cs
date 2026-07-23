using System;
class Aluno
{
    public int Numero { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Aluno(int numero, string nome, int idade)
    {
        Numero = numero;
        Nome = nome;
        Idade = idade;
    }
}

class Program
{
    static void Main(string[] args)
    {
       
        Aluno[] alunos = new Aluno[25];

        int ocupados = 0;

        bool sair = false;

        // Ciclo principal - mantém o menu ativo até o utilizador escolher sair
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(" Sistema de Gestão de Alunos - Turma 3 EISNT");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine();
            Console.WriteLine("1 - Adicionar aluno");
            Console.WriteLine("2 - Listar alunos");
            Console.WriteLine("3 - Procurar aluno");
            Console.WriteLine("4 - Remover aluno");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();
            Console.Write("Opção: ");

            string opcao = Console.ReadLine();

            // Direciona para o método correspondente à opção escolhida
            switch (opcao)
            {
                case "1":
                    AdicionarAluno(alunos, ref ocupados);
                    break;

                case "2":
                    ListarAlunos(alunos, ocupados);
                    break;

                case "3":
                    ProcurarAluno(alunos, ocupados);
                    break;

                case "4":
                    RemoverAluno(alunos, ref ocupados);
                    break;

                case "0":
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    /* Lê um número inteiro, repetindo até ser válido
    O try-catch evita que o programa rebente se o utilizador escrever texto*/
    static int LerInteiroValido(string mensagem)
    {
        int valor = 0;
        bool valido = false;

        while (!valido)
        {
            try
            {
                Console.Write(mensagem);
                valor = int.Parse(Console.ReadLine());
                valido = true;
            }
            catch
            {
                Console.WriteLine("Valor inválido. Tente novamente.");
            }
        }

        return valor;
    }

    // Valida a idade: deve ser um valor realista (entre 1 e 119)
    static int LerIdadeValida(string mensagem)
    {
        int idade;

        while (true)
        {
            idade = LerInteiroValido(mensagem);

            if (idade > 0 && idade < 120)
            {
                return idade;
            }

            Console.WriteLine("Idade inválida. Introduza um valor entre 1 e 119.");
        }
    }

    // Garantia que o nome não ficará vazio
    static string LerNomeValido(string mensagem)
    {
        string nome;

        while (true)
        {
            Console.Write(mensagem);
            nome = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                return nome;
            }

            Console.WriteLine("O nome não pode ficar vazio. Tente novamente.");
        }
    }

    /* Verifica se já existe um aluno com o número indicado
     Percorre o array até à posição ocupada e compara os números*/
    static bool NumeroJaExiste(Aluno[] alunos, int ocupados, int numero)
    {
        for (int i = 0; i < ocupados; i++)
        {
            if (alunos[i].Numero == numero)
            {
                return true;
            }
        }

        return false;
    }
  
    static void AdicionarAluno(Aluno[] alunos, ref int ocupados)
    {
       
        if (ocupados >= alunos.Length)
        {
            Console.WriteLine("O array está cheio.");
            Console.ReadLine();
            return;
        }

        int numero;

        // Repete até o utilizador introduzir um número que não exista
        while (true)
        {
            numero = LerInteiroValido("Número do aluno: ");

            if (!NumeroJaExiste(alunos, ocupados, numero))
            {
                break;
            }

            Console.WriteLine("Já existe um aluno com esse número. Tente novamente.");
        }

        string nome = LerNomeValido("Nome do aluno: ");
        int idade = LerIdadeValida("Idade do aluno: ");

        alunos[ocupados] = new Aluno(numero, nome, idade);
        ocupados++;

        Console.WriteLine("Aluno adicionado com sucesso.");
        Console.ReadLine();
    }

    // Mostra todos os alunos registados
    static void ListarAlunos(Aluno[] alunos, int ocupados)
    {
        if (ocupados == 0)
        {
            Console.WriteLine("Não existem alunos registados.");
            Console.ReadLine();
            return;
        }

        // Percorre apenas as posições ocupadas do array
        for (int i = 0; i < ocupados; i++)
        {
            Console.WriteLine("Aluno " + (i + 1));
            Console.WriteLine("Número: " + alunos[i].Numero);
            Console.WriteLine("Nome: " + alunos[i].Nome);
            Console.WriteLine("Idade: " + alunos[i].Idade);
            Console.WriteLine("-----------------------------------");
        }

        Console.ReadLine();
    }

    // Procura um aluno por número ou nome
    static void ProcurarAluno(Aluno[] alunos, int ocupados)
    {
        if (ocupados == 0)
        {
            Console.WriteLine("Não existem alunos registados.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Procurar por:");
        Console.WriteLine("1 - Número");
        Console.WriteLine("2 - Nome");
        Console.Write("Opção: ");
        string opcao = Console.ReadLine();

        bool encontrado = false;

        if (opcao == "1")
        {
            // Pesquisa por número: compara directamente
            int numero = LerInteiroValido("Número do aluno: ");

            for (int i = 0; i < ocupados; i++)
            {
                if (alunos[i].Numero == numero)
                {
                    Console.WriteLine("Aluno encontrado:");
                    Console.WriteLine("Número: " + alunos[i].Numero);
                    Console.WriteLine("Nome: " + alunos[i].Nome);
                    Console.WriteLine("Idade: " + alunos[i].Idade);
                    encontrado = true;
                    break;
                }
            }
        }
        else if (opcao == "2")
        {
            // Pesquisa por nome: ignora maiúsculas/minúsculas
            Console.Write("Nome do aluno: ");
            string nome = Console.ReadLine();

            for (int i = 0; i < ocupados; i++)
            {
                if (alunos[i].Nome.ToLower() == nome.ToLower())
                {
                    Console.WriteLine("Aluno encontrado:");
                    Console.WriteLine("Número: " + alunos[i].Numero);
                    Console.WriteLine("Nome: " + alunos[i].Nome);
                    Console.WriteLine("Idade: " + alunos[i].Idade);
                    encontrado = true;
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }

        if (!encontrado && (opcao == "1" || opcao == "2"))
        {
            Console.WriteLine("Aluno não encontrado.");
        }

        Console.ReadLine();
    }

    // Remove um aluno do array
    static void RemoverAluno(Aluno[] alunos, ref int ocupados)
    {
        if (ocupados == 0)
        {
            Console.WriteLine("Não existem alunos registados.");
            Console.ReadLine();
            return;
        }

        int numero = LerInteiroValido("Número do aluno a remover: ");
        int indice = -1;

        // Procura o índice do aluno no array
        for (int i = 0; i < ocupados; i++)
        {
            if (alunos[i].Numero == numero)
            {
                indice = i;
                break;
            }
        }

        if (indice == -1)
        {
            Console.WriteLine("Aluno não encontrado.");
            Console.ReadLine();
            return;
        }

        // Mostra os dados do aluno antes de confirmar a remoção
        Console.WriteLine();
        Console.WriteLine("Aluno encontrado:");
        Console.WriteLine("Número: " + alunos[indice].Numero);
        Console.WriteLine("Nome: " + alunos[indice].Nome);
        Console.WriteLine("Idade: " + alunos[indice].Idade);
        Console.WriteLine();
        Console.Write("Tem a certeza que deseja remover este aluno? (S/N): ");

        string confirmacao = Console.ReadLine();

        // Se não confirmar, cancela a operação
        if (confirmacao.ToUpper() != "S")
        {
            Console.WriteLine("Operação cancelada.");
            Console.ReadLine();
            return;
        }

        // Desloca todos os alunos seguintes uma posição para trás, evita que o array fico com uma "casinha" vazia
        for (int i = indice; i < ocupados - 1; i++)
        {
            alunos[i] = alunos[i + 1];
        }

        // Limpa a última posição e actualiza o contador
        alunos[ocupados - 1] = null;
        ocupados--;

        Console.WriteLine("Aluno removido com sucesso.");
        Console.ReadLine();
    }
}
