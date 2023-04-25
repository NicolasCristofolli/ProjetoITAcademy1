using Microsoft.VisualBasic.FileIO;


namespace TIAcademy {
    public class Caminhao {
        public double PesoSuportado { get; }
        public double KmHora { get; }
        public string Nome { get; }


        public Caminhao(double pesoSuportado, double kmHora, string nome) {
            PesoSuportado = pesoSuportado;
            KmHora = kmHora;
            Nome = nome;
        }

    }

    public class CaminhaoPequeno : Caminhao {
        public CaminhaoPequeno() : base(1000, 4.87, "pequeno") { }
    }

    class CaminhaoMedio : Caminhao {
        public CaminhaoMedio() : base(4000, 11.92, "medio") { }
    }

    class CaminhaoGrande : Caminhao {
        public CaminhaoGrande() : base(10000, 27.44, "grande") { }
    }
    public class Produto {
        public double peso;
        public int quantidade;

        public double pesoTotal => peso * quantidade;

        public Produto(double Peso, int Quantidade) {
            peso = Peso;
            quantidade = Quantidade;
        }

        public Produto() {

        }

    }
    internal class Principal {
        public static void Main(string[] args) {
            string filePath = @"D:\Users\nickc\Downloads\DNIT-Distancias.csv";

            CaminhaoPequeno pequeno = new CaminhaoPequeno();
            CaminhaoMedio medio = new CaminhaoMedio();
            CaminhaoGrande grande = new CaminhaoGrande();

            double pedagio = 0;
            bool continuar = true;
            string palavraChave = "sair";
            while (continuar) {
                Console.WriteLine("============TRANSPORTADORA DELY============");
                Console.WriteLine("           (DIGITE APENAS NUMEROS)");
                Console.WriteLine("                   MENU");
                Console.WriteLine("1. [Consultar trechos x modalidade]");
                Console.WriteLine("2. [Cadastrar transporte]");
                Console.WriteLine("3. [Sair]");
                Console.WriteLine(" ");
                Console.WriteLine("  *Caso queira sair a qualquer momento digite 'sair'");

                int opcao;
                if (int.TryParse(Console.ReadLine(), out opcao))

                    //[Consultar trechos x modalidade]
                    if (opcao == 1) {
                        using (TextFieldParser parser = new TextFieldParser(filePath)) {
                            parser.TextFieldType = FieldType.Delimited;
                            parser.SetDelimiters(";");
                            string[] headers = parser.ReadFields();
                            Console.WriteLine("As cidades Disponiveis sao: ");
                            foreach (string header in headers) {
                                Console.WriteLine(header);
                            }
                            Console.WriteLine("".PadLeft(60, '='));
                            Console.WriteLine("Digite a primeira cidade:");
                            string cidade1 = Console.ReadLine().ToUpper();
                            if (cidade1.ToLower() == palavraChave) {
                                break;
                            } else {
                                // Verificar se a cidade1 existe  no arquivo CSV
                                bool cidade1Existe = headers.Contains(cidade1);
                                while (!cidade1Existe) {
                                    Console.WriteLine("Cidade não encontrada. Digite novamente:");
                                    cidade1 = Console.ReadLine().ToUpper();
                                    cidade1Existe = headers.Contains(cidade1);
                                }

                                Console.WriteLine("Digite a segunda cidade:");
                                string cidade2 = Console.ReadLine().ToUpper();
                                if (cidade2.ToLower() == palavraChave) {
                                    break;
                                } else {

                                    // Verificar se a cidade2 existe  no arquivo CSV
                                    bool cidade2Existe = headers.Contains(cidade2);
                                    while (!cidade2Existe) {
                                        Console.WriteLine("Cidade não encontrada. Digite novamente:");
                                        cidade2 = Console.ReadLine().ToUpper();
                                        cidade2Existe = headers.Contains(cidade2);
                                    }

                                    // Encontrar o índice das colunas para as duas cidades
                                    int cidade1Index = Array.IndexOf(headers, cidade1);
                                    int cidade2Index = Array.IndexOf(headers, cidade2);

                                    // Ler a linha correspondente da coluna da cidade2
                                    List<string> cidade2Distancias = new List<string>();
                                    while (!parser.EndOfData) {
                                        string[] fields = parser.ReadFields();
                                        cidade2Distancias.Add(fields[cidade2Index]);
                                    }
                                    int tamanhoCaminhao = 0;
                                    bool tamanhoCaminhaoValido = false;
                                    Console.WriteLine("".PadLeft(60, '='));

                                    while (!tamanhoCaminhaoValido) {
                                        Console.WriteLine("Qual tamanho do caminhão você gostaria de utilizar? DIGITE [1] PARA PEQUENO, [2] PARA MÉDIO, [3] PARA GRANDE");
                                        Console.WriteLine("- Caminhão Pequeno aguenta até 1 Tonelada e Custa R$4,87 por Km/Hora");
                                        Console.WriteLine("- Caminhão Médio aguenta até 4 Toneladas e Custa R$11,92 por Km/Hora");
                                        Console.WriteLine("- Caminhão Grande aguenta até 10 Toneladas e Custa R$27,44 por Km/Hora");

                                        string tamanhoCaminhaoStr = Console.ReadLine();
                                        if (tamanhoCaminhaoStr.ToLower() == palavraChave) {
                                            break;
                                        } else {
                                            if (int.TryParse(tamanhoCaminhaoStr, out tamanhoCaminhao)) {
                                                if (tamanhoCaminhao >= 1 && tamanhoCaminhao <= 3) {
                                                    tamanhoCaminhaoValido = true;
                                                } else {
                                                    Console.WriteLine("Valor inválido. Digite 1, 2 ou 3.");
                                                }
                                            } else {
                                                Console.WriteLine("Valor inválido. Digite 1, 2 ou 3.");
                                            }
                                        }
                                    }
                                    Console.WriteLine("".PadLeft(60, '='));
                                    while (true) {
                                        Console.WriteLine($"Deseja adicionar o preço do pedágio?? (sim/nao)");
                                        string resposta = Console.ReadLine().ToLower();
                                        if (resposta == palavraChave) {
                                            break;
                                        } else if (resposta == "sim") {
                                            bool entradaValida = false;
                                            while (!entradaValida) {
                                                Console.WriteLine("Qual é o preço do pedágio?");
                                                string input = Console.ReadLine();
                                                entradaValida = decimal.TryParse(input, out decimal preco);
                                                if (!entradaValida || preco < 0) {
                                                    Console.WriteLine("Digite apenas números positivos.");
                                                    entradaValida = false;
                                                } else {
                                                    pedagio = (double)preco;
                                                }
                                            }
                                            break;
                                        } else if (resposta == "nao") {
                                            break;
                                        } else
                                            Console.WriteLine("Resposta inválida, por favor digite sim ou nao.");
                                    }
                                    Caminhao caminhaoEscolhido;

                                    switch (tamanhoCaminhao) {
                                        case 1:
                                            caminhaoEscolhido = pequeno;
                                            break;
                                        case 2:
                                            caminhaoEscolhido = medio;
                                            break;
                                        case 3:
                                            caminhaoEscolhido = grande;
                                            break;
                                        default:
                                            throw new ArgumentException("Tamanho de caminhão inválido");
                                    }
                                    // Obter a linha correspondente da coluna da cidade2 baseado na posição da coluna da cidade1
                                    string cidade2DistanciaStr = cidade2Distancias[cidade1Index];
                                    if (Double.TryParse(cidade2DistanciaStr, out double cidade2Distancia)) {
                                        Console.WriteLine("".PadLeft(60, '='));
                                        Console.WriteLine($"A distancia de {cidade1} para {cidade2} é de {cidade2Distancia} KM e usando o caminhao {caminhaoEscolhido.Nome.ToUpper()}, isso custará R$ {cidade2Distancia * caminhaoEscolhido.KmHora + pedagio:F2} (valor total somado com o custo do pedágio (R$ {pedagio}).");
                                    } else {
                                        Console.WriteLine("Erro: não foi possível converter a distância para um número válido.");
                                        Console.ReadKey();
                                        return;
                                    }
                                }
                            }
                        }
                    } else if (opcao == 2) {
                        using (TextFieldParser parser = new TextFieldParser(filePath)) {
                            Console.WriteLine("[Cadastrar transporte]");
                            parser.TextFieldType = FieldType.Delimited;
                            parser.SetDelimiters(";");
                            string[] headers = parser.ReadFields();
                            Console.WriteLine("As cidades disponíveis são:");
                            foreach (string header in headers) {
                                Console.WriteLine(header);
                            }
                            Console.WriteLine("".PadLeft(60, '='));
                            Console.WriteLine("Digite a primeira cidade:");
                            string cidade1 = Console.ReadLine().ToUpper();
                            if (cidade1.ToLower() == palavraChave) {
                                break;
                            } else {

                                // Verificar se a cidade1 existe  no arquivo CSV
                                bool cidade1Existe = headers.Contains(cidade1);
                                while (!cidade1Existe) {
                                    Console.WriteLine("Cidade não encontrada. Digite novamente:");
                                    cidade1 = Console.ReadLine().ToUpper();
                                    cidade1Existe = headers.Contains(cidade1);
                                }
                            }

                            Console.WriteLine("Digite a segunda cidade:");
                            string cidade2 = Console.ReadLine().ToUpper();
                            if (cidade2.ToLower() == palavraChave) {
                                break;
                            } else {
                                // Verificar se a cidade2 existe  no arquivo CSV
                                bool cidade2Existe = headers.Contains(cidade2);
                                while (!cidade2Existe) {
                                    Console.WriteLine("Cidade não encontrada. Digite novamente:");
                                    cidade2 = Console.ReadLine().ToUpper();
                                    cidade2Existe = headers.Contains(cidade2);
                                }
                            }


                            // Encontrar o índice das colunas para as duas cidades
                            int cidade1Index = Array.IndexOf(headers, cidade1);
                            int cidade2Index = Array.IndexOf(headers, cidade2);

                            // Ler a linha correspondente da coluna da cidade2
                            List<string> cidade2Distancias = new List<string>();
                            while (!parser.EndOfData) {
                                string[] fields = parser.ReadFields();
                                cidade2Distancias.Add(fields[cidade2Index]);
                            }
                            var produtos = new Dictionary<string, double>() {
                         {"celular", 0.5},
                         {"geladeira", 60.0},
                         {"freezer", 100.0},
                         {"cadeira", 5.0},
                         {"luminaria", 0.8},
                         {"lavadoraRoupa", 120.0}
                    };
                            var quantidades = new Dictionary<string, int>();
                            Console.WriteLine("".PadLeft(60, '='));
                            Console.WriteLine("      (MÁXIMO DE 30 TONELADAS EM PRODUTOS)");
                            foreach (var produto in produtos) {
                                bool entradaValida = false;
                                int quantidade = 0;
                                while (!entradaValida) {
                                    Console.WriteLine($"Quantos {produto.Key}(s/es/as) gostaria de enviar?");
                                    string input = Console.ReadLine();
                                    if (input.ToLower() == palavraChave) {
                                        break;
                                    } else {
                                        entradaValida = int.TryParse(input, out quantidade);
                                        if (!entradaValida) {
                                            Console.WriteLine("Digite apenas números.");
                                        } else if (quantidade < 0) {
                                            Console.WriteLine("Digite apenas números positivos.");
                                            entradaValida = false;
                                        }
                                        quantidades[produto.Key] = quantidade;
                                    }
                                }
                            }
                            Console.WriteLine($"         Produtos transportados:");
                            foreach (var item in quantidades) {
                                Console.WriteLine($"* {item.Key}: {item.Value}");
                            }
                            int totalProdutos = quantidades.Sum(q => q.Value);

                            Caminhao[] caminhoes = { pequeno, medio, grande };

                            double pesoTotal = produtos.Sum(p => p.Value * quantidades[p.Key]);

                            //double custoTotalPorProduto = pesoTotal / ;
                            int melhorCaminhaoPequeno = 0, melhorCaminhaoMedio = 0, melhorCaminhaoGrande = 0;
                            int melhorCaminhaoPequeno2 = 0, melhorCaminhaoMedio2 = 0, melhorCaminhaoGrande2 = 0;

                            // Obter a linha correspondente da coluna da cidade2 baseado na posição da coluna da cidade1
                            string cidade2DistanciaStr = cidade2Distancias[cidade1Index];
                            if (Double.TryParse(cidade2DistanciaStr, out double cidade2Distancia)) {
                            } else {
                                Console.WriteLine("Erro: não foi possível converter a distância para um número válido.");
                                Console.ReadKey();
                                return;
                            }

                            //CALCULO PARA VER QUAL A MELHOR COMBINACAO DE CAMINHAO
                            switch (pesoTotal) {
                                case <= 1000:
                                    melhorCaminhaoPequeno = 1;
                                    melhorCaminhaoMedio = 0;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 2000:
                                    melhorCaminhaoPequeno = 2;
                                    melhorCaminhaoMedio = 0;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 4000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 1;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 5000:
                                    melhorCaminhaoPequeno = 1;
                                    melhorCaminhaoMedio = 1;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 6000:
                                    melhorCaminhaoPequeno = 2;
                                    melhorCaminhaoMedio = 1;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 8000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 2;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 10000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 0;
                                    melhorCaminhaoGrande = 1;
                                    break;
                                case <= 11000:
                                    melhorCaminhaoPequeno = 1;
                                    melhorCaminhaoMedio = 0;
                                    melhorCaminhaoGrande = 1;
                                    break;
                                case <= 12000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 3;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 14000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 1;
                                    melhorCaminhaoGrande = 1;
                                    break;
                                case <= 15000:
                                    melhorCaminhaoPequeno = 1;
                                    melhorCaminhaoMedio = 1;
                                    melhorCaminhaoGrande = 1;
                                    break;
                                case <= 16000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 4;
                                    melhorCaminhaoGrande = 0;
                                    break;
                                case <= 18000:
                                    melhorCaminhaoPequeno = 1;
                                    melhorCaminhaoMedio = 2;
                                    melhorCaminhaoGrande = 1;
                                    break;
                                case <= 20000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 0;
                                    melhorCaminhaoGrande = 2;
                                    break;
                                case <= 21000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 0;
                                    melhorCaminhaoGrande = 2;
                                    break;
                                case <= 23000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 3;
                                    melhorCaminhaoGrande = 1;
                                    break;
                                case <= 24000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 1;
                                    melhorCaminhaoGrande = 2;
                                    break;
                                case <= 25000:
                                    melhorCaminhaoPequeno = 1;
                                    melhorCaminhaoMedio = 1;
                                    melhorCaminhaoGrande = 2;
                                    break;
                                case <= 26000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 4;
                                    melhorCaminhaoGrande = 1;
                                    break;
                                case <= 28000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 2;
                                    melhorCaminhaoGrande = 2;
                                    break;
                                case <= 30000:
                                    melhorCaminhaoPequeno = 0;
                                    melhorCaminhaoMedio = 0;
                                    melhorCaminhaoGrande = 3;
                                    break;
                                case >= 30001:
                                    Console.WriteLine("!!!!!!!!!!!LIMITE DE PESO EXCEDIDO!!!!!!!!!!!");
                                    break;
                            }
                            double custoTotalEntrega = cidade2Distancia * (((melhorCaminhaoPequeno * pequeno.KmHora) + (melhorCaminhaoMedio * medio.KmHora) + (melhorCaminhaoGrande * grande.KmHora)));
                            string resposta;
                            double custoTotalPorKM = custoTotalEntrega / cidade2Distancia;

                            //PERGUNTA CIDADE3
                            while (true) {
                                Console.WriteLine("".PadLeft(60, '='));
                                Console.WriteLine($"Deseja descarregar alguns produtos em {cidade2} e levar o restante para uma terceira cidade? (sim/Aperte qualquer tecla)");
                                resposta = Console.ReadLine().ToLower();
                                if (resposta.ToLower() == palavraChave) {
                                    break;
                                }

                                if (resposta == "sim") {
                                    Console.WriteLine("Digite a segunda cidade:");
                                    string cidade3 = Console.ReadLine().ToUpper();
                                    if (cidade3.ToLower() == palavraChave) {
                                        break;
                                    } else {
                                        // Verificar se a cidade2 existe  no arquivo CSV
                                        bool cidade3Existe = headers.Contains(cidade3);
                                        while (!cidade3Existe) {
                                            Console.WriteLine("Cidade não encontrada. Digite novamente:");
                                            cidade2 = Console.ReadLine().ToUpper();
                                            cidade3Existe = headers.Contains(cidade3);
                                        }
                                    }

                                    // Encontrar o índice das colunas para as duas cidades
                                    int cidade3Index = Array.IndexOf(headers, cidade3);

                                    // Ler a linha correspondente da coluna da cidade3
                                    List<string> cidade3Distancias = new List<string>();
                                    using (TextFieldParser parser2 = new TextFieldParser(filePath)) {
                                        parser2.TextFieldType = FieldType.Delimited;
                                        parser2.SetDelimiters(";");
                                        string[] headers2 = parser2.ReadFields();
                                        bool primeiraLinha = true;
                                        while (!parser2.EndOfData) {
                                            string[] fields = parser2.ReadFields();
                                            cidade3Distancias.Add(fields[cidade3Index]);
                                        }
                                    }

                                    // Segunda pergunta - selecionar produtos para retirar
                                    foreach (var produto in produtos) {
                                        int quantidadeInicial = quantidades[produto.Key];
                                        int quantidadeRetirada = 0;
                                        while (true) {
                                            Console.WriteLine($"Quantos {produto.Key}(es) que você gostaria de DESCARREGAR em {cidade2}?");
                                            bool entradaValida = int.TryParse(Console.ReadLine(), out quantidadeRetirada);

                                            if (!entradaValida) {
                                                Console.WriteLine("Digite apenas números. Tente novamente.");
                                            } else if (quantidadeRetirada > quantidadeInicial) {
                                                Console.WriteLine($"Você não pode retirar mais do que {quantidadeInicial} {produto.Key}(e)s. Tente novamente.");
                                            } else if (quantidadeRetirada < 0) {
                                                Console.WriteLine("Digite apenas números positivos.");
                                                entradaValida = false;
                                            } else {
                                                break;
                                            }
                                        }
                                        quantidades[produto.Key] -= quantidadeRetirada;
                                    }
                                    Console.WriteLine("".PadLeft(60, '='));
                                    Console.WriteLine($"         Produtos transportados:");
                                    foreach (var item in quantidades) {
                                        Console.WriteLine($"* {item.Key}: {item.Value}");
                                    }
                                    Console.WriteLine("".PadLeft(60, '='));
                                    while (true) {
                                        Console.WriteLine($"Deseja adicionar o preço do pedágio?? (sim/nao)");
                                        string respostaPedagio = Console.ReadLine().ToLower();
                                        if (respostaPedagio == palavraChave) {
                                            break;
                                        } else if (respostaPedagio == "sim") {
                                            bool entradaValida = false;
                                            while (!entradaValida) {
                                                Console.WriteLine("Qual é o preço do pedágio?");
                                                string input = Console.ReadLine();
                                                entradaValida = decimal.TryParse(input, out decimal preco);
                                                if (!entradaValida || preco < 0) {
                                                    Console.WriteLine("Digite apenas números positivos.");
                                                    entradaValida = false;
                                                } else {
                                                    pedagio = (double)preco;
                                                }
                                            }
                                            break;
                                        } else if (respostaPedagio == "nao") {
                                            break;
                                        } else
                                            Console.WriteLine("Resposta inválida, por favor digite sim ou nao.");
                                    }
                                    totalProdutos = quantidades.Sum(q => q.Value);
                                    // Obter a linha correspondente da coluna da cidade2 baseado na posição da coluna da cidade3
                                    string cidade3DistanciaStr = cidade3Distancias[cidade2Index];
                                    if (Double.TryParse(cidade3DistanciaStr, out double cidade3Distancia)) {
                                    } else {
                                        break;
                                    }

                                    //CALCULO PARA VER A MELHOR COMBINACAO DE CAMINHOES CIDADE 2 PARA 3
                                    double pesoTotal2 = produtos.Sum(p => p.Value * quantidades[p.Key] - pesoTotal);
                                    switch (pesoTotal2) {
                                        case <= 1000:
                                            melhorCaminhaoPequeno2 = 1;
                                            melhorCaminhaoMedio2 = 0;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 2000:
                                            melhorCaminhaoPequeno2 = 2;
                                            melhorCaminhaoMedio2 = 0;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 4000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 1;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 5000:
                                            melhorCaminhaoPequeno2 = 1;
                                            melhorCaminhaoMedio2 = 1;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 6000:
                                            melhorCaminhaoPequeno2 = 2;
                                            melhorCaminhaoMedio2 = 1;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 8000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 2;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 10000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 0;
                                            melhorCaminhaoGrande2 = 1;
                                            break;
                                        case <= 11000:
                                            melhorCaminhaoPequeno2 = 1;
                                            melhorCaminhaoMedio2 = 0;
                                            melhorCaminhaoGrande2 = 1;
                                            break;
                                        case <= 12000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 3;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 14000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 1;
                                            melhorCaminhaoGrande2 = 1;
                                            break;
                                        case <= 15000:
                                            melhorCaminhaoPequeno2 = 1;
                                            melhorCaminhaoMedio2 = 1;
                                            melhorCaminhaoGrande2 = 1;
                                            break;
                                        case <= 16000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 4;
                                            melhorCaminhaoGrande2 = 0;
                                            break;
                                        case <= 18000:
                                            melhorCaminhaoPequeno2 = 1;
                                            melhorCaminhaoMedio2 = 2;
                                            melhorCaminhaoGrande2 = 1;
                                            break;
                                        case <= 20000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 0;
                                            melhorCaminhaoGrande2 = 2;
                                            break;
                                        case <= 21000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 0;
                                            melhorCaminhaoGrande2 = 2;
                                            break;
                                        case <= 23000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 3;
                                            melhorCaminhaoGrande2 = 1;
                                            break;
                                        case <= 24000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 1;
                                            melhorCaminhaoGrande2 = 2;
                                            break;
                                        case <= 25000:
                                            melhorCaminhaoPequeno2 = 1;
                                            melhorCaminhaoMedio2 = 1;
                                            melhorCaminhaoGrande2 = 2;
                                            break;
                                        case <= 26000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 4;
                                            melhorCaminhaoGrande2 = 1;
                                            break;
                                        case <= 28000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 2;
                                            melhorCaminhaoGrande2 = 2;
                                            break;
                                        case <= 30000:
                                            melhorCaminhaoPequeno2 = 0;
                                            melhorCaminhaoMedio2 = 0;
                                            melhorCaminhaoGrande2 = 3;
                                            break;
                                    }
                                    double custoTotalEntrega2 = cidade3Distancia * (((melhorCaminhaoPequeno2 * pequeno.KmHora) + (melhorCaminhaoMedio2 * medio.KmHora) + (melhorCaminhaoGrande2 * grande.KmHora)));
                                    double custoTotalPorKM2 = custoTotalEntrega2 / cidade3Distancia;

                                    //CIDADE 1 PARA 2
                                    Console.WriteLine("".PadLeft(60, '='));
                                    Console.WriteLine("              DADOS ESTATISTICOS DO PRIMEIRO TRANSPORTE ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine($"- A distância de {cidade1} para {cidade2} é de {cidade2Distancia} KM.");
                                    Console.WriteLine($"- Os caminhoes usados: {melhorCaminhaoPequeno} Pequeno, {melhorCaminhaoMedio} medio, {melhorCaminhaoGrande} grande");
                                    Console.WriteLine($"- O valor total do primeiro transporte foi de R$ {custoTotalEntrega:F2}");
                                    Console.WriteLine($"- O valor total do custo médio por KM foi de R$ {custoTotalPorKM:F2}");
                                    Console.WriteLine($"- O custo total para cada modalidade no primeiro transporte foi de R$:");
                                    Console.WriteLine($"* Caminhao Pequeno: R$ {cidade2Distancia * melhorCaminhaoPequeno * pequeno.KmHora:F2}");
                                    Console.WriteLine($"* Caminhao Medio: R$ {cidade2Distancia * melhorCaminhaoMedio * medio.KmHora:F2}");
                                    Console.WriteLine($"* Caminhao Grande: R$ {cidade2Distancia * melhorCaminhaoGrande * grande.KmHora:F2}");

                                    //CIDADE 2 PARA 3
                                    Console.WriteLine("".PadLeft(60, '='));
                                    Console.WriteLine("              DADOS ESTATISTICOS DO SEGUNDO TRANSPORTE ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine($"A distância de {cidade2} para {cidade3} é de {cidade3Distancia} KM.");
                                    Console.WriteLine($"- Os caminhoes usados: {melhorCaminhaoPequeno2} Pequeno, {melhorCaminhaoMedio2} medio, {melhorCaminhaoGrande2} grande");
                                    Console.WriteLine($"- O valor total do segundo transporte foi de R$ {custoTotalEntrega2:F2}");
                                    Console.WriteLine($"- O valor total do custo médio por KM foi de R$ {custoTotalPorKM2:F2}");
                                    Console.WriteLine($"- O custo total para cada modalidade no segundo transporte foi de R$:");
                                    Console.WriteLine($"* Caminhao Pequeno: R$ {cidade3Distancia * melhorCaminhaoPequeno2 * pequeno.KmHora:F2}");
                                    Console.WriteLine($"* Caminhao Medio: R$ {cidade3Distancia * melhorCaminhaoMedio2 * medio.KmHora:F2}");
                                    Console.WriteLine($"* Caminhao Grande: R$ {cidade3Distancia * melhorCaminhaoGrande2 * grande.KmHora:F2}");

                                    // GERAL
                                    Console.WriteLine("".PadLeft(60, '='));
                                    Console.WriteLine("              DADOS ESTATISTICOS GERAIS ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine($"- O total da distância percorrida foi de {cidade2Distancia + cidade3Distancia} KM");
                                    Console.WriteLine($"- O custo total das duas entregas foi de R$ {pedagio + custoTotalEntrega + custoTotalEntrega2:F2} (valor ");
                                    Console.WriteLine($"- Os caminhoes usados: {melhorCaminhaoPequeno + melhorCaminhaoPequeno2} Pequeno, {melhorCaminhaoMedio + melhorCaminhaoMedio2} medio, {melhorCaminhaoGrande + melhorCaminhaoGrande2} grande (valor total somado com o custo do pedágio (R$ {pedagio})");
                                    Console.WriteLine($"- O valor total do custo médio por KM foi de R$ {custoTotalPorKM + custoTotalPorKM2:F2}");
                                    Console.WriteLine("".PadLeft(60, '='));
                                    Console.WriteLine($"-  Preço o custo médio por tipo de produto: ");
                                    Console.WriteLine($"  caminhao pequeno / caminhao medio/ caminhao grande");
                                    Console.WriteLine($"* Celular: R$0,10 / R$0,04 / R$0.18 ");
                                    Console.WriteLine($"* Geladeira: R$12,32 / R$5,03 / R$2,18 ");
                                    Console.WriteLine($"* Freezer: R$20,53 / R$8,38 / R$3,64 ");
                                    Console.WriteLine($"* Cadeira: R$1,02 / R$0,41 / R$0,18 ");
                                    Console.WriteLine($"* Luminária: R$0,16 / R$0,06 / R$0.02 ");
                                    Console.WriteLine($"* Lavadora de roupa: R$24,64 / R$10,06 / R$4,37 ");
                                    Console.WriteLine($"Total de produtos pedidos: {totalProdutos}");
                                    break;

                                } else if (resposta == "nao") ;
                                { //CASO NAO TENHA TERCEIRA CIDADE
                                    Console.WriteLine("".PadLeft(60, '='));
                                    while (true) {
                                        Console.WriteLine($"Deseja adicionar o preço do pedágio?? (sim/nao)");
                                        string resposta2 = Console.ReadLine().ToLower();
                                        if (resposta2 == palavraChave) {
                                            break;
                                        } else if (resposta2 == "sim") {
                                            bool entradaValida = false;
                                            while (!entradaValida) {
                                                Console.WriteLine("Qual é o preço do pedágio?");
                                                string input = Console.ReadLine();
                                                entradaValida = decimal.TryParse(input, out decimal preco);
                                                if (!entradaValida || preco < 0) {
                                                    Console.WriteLine("Digite apenas números positivos.");
                                                    entradaValida = false;
                                                } else {
                                                    pedagio = (double)preco;
                                                }
                                            }
                                            break;
                                        } else if (resposta2 == "nao") {
                                            break;
                                        } else
                                            Console.WriteLine("Resposta inválida, por favor digite sim ou nao.");
                                    }

                                    Console.WriteLine("".PadLeft(60, '='));
                                    Console.WriteLine("              DADOS ESTATISTICOS GERAIS ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine($"- A distância de {cidade1} para {cidade2} é de {cidade2Distancia} KM.");
                                    Console.WriteLine($"- Os caminhoes usados: {melhorCaminhaoPequeno} Pequeno, {melhorCaminhaoMedio} medio, {melhorCaminhaoGrande} grande");
                                    Console.WriteLine($"- O valor total foi de R$ {custoTotalEntrega:F2} (valor total somado com o custo do pedágio (R$ {pedagio})");
                                    Console.WriteLine($"- O valor total do custo médio por KM foi de R$ {custoTotalPorKM:F2}");
                                    Console.WriteLine($"- O custo total para cada modalidade no primeiro transporte foi de R$:");
                                    Console.WriteLine($"* Caminhao Pequeno: R$ {cidade2Distancia * melhorCaminhaoPequeno * pequeno.KmHora:F2}");
                                    Console.WriteLine($"* Caminhao Medio: R$ {cidade2Distancia * melhorCaminhaoMedio * medio.KmHora:F2}");
                                    Console.WriteLine($"* Caminhao Grande: R$ {cidade2Distancia * melhorCaminhaoGrande * grande.KmHora:F2}");
                                    Console.WriteLine("".PadLeft(60, '='));
                                    Console.WriteLine($"-  Preço o custo médio por tipo de produto: ");
                                    Console.WriteLine($"  caminhao pequeno / caminhao medio/ caminhao grande");
                                    Console.WriteLine($"* Celular: R$0,10 / R$0,04 / R$0.18 ");
                                    Console.WriteLine($"* Geladeira: R$12,32 / R$5,03 / R$2,18 ");
                                    Console.WriteLine($"* Freezer: R$20,53 / R$8,38 / R$3,64 ");
                                    Console.WriteLine($"* Cadeira: R$1,02 / R$0,41 / R$0,18 ");
                                    Console.WriteLine($"* Luminária: R$0,16 / R$0,06 / R$0.02 ");
                                    Console.WriteLine($"* Lavadora de roupa: R$24,64 / R$10,06 / R$4,37 ");
                                    break;


                                }
                            }
                        }
                    } else if (opcao == 3) {

                        Console.WriteLine("Saindo...");
                        continuar = false;
                    } else {
                        Console.WriteLine("Opção inválida!");
                    }

                Console.WriteLine();
            }
        }
    }
}
