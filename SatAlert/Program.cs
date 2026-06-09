using SatAlert.Interfaces;
using SatAlert.Models;
using SatAlert.Repositories;
using SatAlert.Services;


// O Program depende da interface ISensorService, não da implementação concreta

ISensorService sensorService = new SensorService();
AlertaRepository alertaRepo = new AlertaRepository();
List<Satelite> satelites = new List<Satelite>();

bool executando = true;


Console.Clear();
ExibirCabecalho();

while (executando)
{
    ExibirMenu();

    string? opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarSatelite();
                break;
            case "2":
                RegistrarLeitura();
                break;
            case "3":
                ListarSatelites();
                break;
            case "4":
                VerAlertas();
                break;
            case "5":
                FiltrarAlertas();
                break;
            case "0":
                executando = false;
                Console.WriteLine("\n  Encerrando SatAlert. Até logo!\n");
                break;
            default:
                Console.WriteLine("\n  Opção inválida. Tente novamente.");
                break;
        }
    }
    catch (Exception ex)
    {
      
        Console.WriteLine($"\n  [ERRO] {ex.Message}");
    }

    if (executando)
    {
        Console.WriteLine("\n  Pressione ENTER para continuar...");
        Console.ReadLine();
        Console.Clear();
        ExibirCabecalho();
    }
}



void ExibirCabecalho()
{
    Console.WriteLine("╔══════════════════════════════════════════════╗");
    Console.WriteLine("║        SatAlert - Monitoramento Espacial     ║");
    Console.WriteLine("╚══════════════════════════════════════════════╝");
    Console.WriteLine();
}

void ExibirMenu()
{
    Console.WriteLine("  [1] Cadastrar Satélite");
    Console.WriteLine("  [2] Registrar Leitura de Sensor");
    Console.WriteLine("  [3] Listar Satélites");
    Console.WriteLine("  [4] Ver Todos os Alertas");
    Console.WriteLine("  [5] Filtrar Alertas por Nível");
    Console.WriteLine("  [0] Sair");
    Console.Write("\n  Escolha uma opção: ");
}

void CadastrarSatelite()
{
    Console.WriteLine("\n  ── Cadastrar Satélite ──");
    Console.WriteLine("  Tipo: [1] Climático  [2] Agrícola");
    Console.Write("  Escolha: ");
    string? tipo = Console.ReadLine();

    Console.Write("  Nome do satélite: ");
    string nome = Console.ReadLine() ?? "Sem nome";

    Console.Write("  Latitude (-90 a 90): ");
    if (!double.TryParse(Console.ReadLine(), out double lat))
        throw new FormatException("Latitude inválida. Use números (ex: -23.5)");

    Console.Write("  Longitude (-180 a 180): ");
    if (!double.TryParse(Console.ReadLine(), out double lon))
        throw new FormatException("Longitude inválida. Use números (ex: -46.6)");

    var coord = new Coordenada(lat, lon);

    if (tipo == "1")
    {
        Console.Write("  Limite de temperatura crítica (°C): ");
        if (!double.TryParse(Console.ReadLine(), out double limite))
            throw new FormatException("Valor de temperatura inválido.");

        satelites.Add(new SateliteClimatico(nome, coord, limite));
        Console.WriteLine($"\n  ✔ Satélite Climático '{nome}' cadastrado com sucesso!");
    }
    else if (tipo == "2")
    {
        Console.Write("  Região monitorada: ");
        string regiao = Console.ReadLine() ?? "Indefinida";

        Console.Write("  Limite de umidade crítica (%): ");
        if (!double.TryParse(Console.ReadLine(), out double limite))
            throw new FormatException("Valor de umidade inválido.");

        satelites.Add(new SateliteAgricola(nome, coord, regiao, limite));
        Console.WriteLine($"\n  ✔ Satélite Agrícola '{nome}' cadastrado com sucesso!");
    }
    else
    {
        throw new ArgumentException("Tipo de satélite inválido.");
    }
}

void RegistrarLeitura()
{
    if (satelites.Count == 0)
    {
        Console.WriteLine("\n  Nenhum satélite cadastrado. Cadastre um primeiro.");
        return;
    }

    Console.WriteLine("\n  ── Registrar Leitura ──");
    ListarSatelites();

    Console.Write("  ID do satélite: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
        throw new FormatException("ID inválido.");

    Satelite? sat = satelites.Find(s => s.Id == id);
    if (sat == null)
        throw new KeyNotFoundException($"Satélite com ID {id} não encontrado.");

    string tipoLeitura = sat is SateliteClimatico ? "temperatura (°C)" : "umidade (%)";
    Console.Write($"  Informe o valor de {tipoLeitura}: ");

    if (!double.TryParse(Console.ReadLine(), out double valor))
        throw new FormatException("Valor de leitura inválido.");

    // Injeção de dependência em uso: chamamos via interface
    var alerta = sensorService.RegistrarLeitura(sat, valor, tipoLeitura);

    if (alerta != null)
        alertaRepo.Adicionar(alerta);
}

void ListarSatelites()
{
    Console.WriteLine("\n  ── Satélites Cadastrados ──");
    if (satelites.Count == 0)
    {
        Console.WriteLine("  Nenhum satélite cadastrado.");
        return;
    }

    foreach (var s in satelites)
        Console.WriteLine($"  {s.ObterResumo()}");
}

void VerAlertas()
{
    Console.WriteLine("\n  ── Alertas Gerados ──");
    sensorService.ExibirHistorico();
    Console.WriteLine($"\n  Total: {alertaRepo.ContarAlertas()} alerta(s)");
}

void FiltrarAlertas()
{
    Console.WriteLine("\n  ── Filtrar por Nível ──");
    Console.WriteLine("  [1] Baixo  [2] Médio  [3] Crítico");
    Console.Write("  Escolha: ");
    string? op = Console.ReadLine();

    NivelAlerta nivel = op switch
    {
        "1" => NivelAlerta.Baixo,
        "2" => NivelAlerta.Medio,
        "3" => NivelAlerta.Critico,
        _ => throw new ArgumentException("Nível inválido.")
    };

    var filtrados = alertaRepo.ObterPorNivel(nivel);
    Console.WriteLine($"\n  Alertas nível {nivel}:");

    if (filtrados.Count == 0)
        Console.WriteLine("  Nenhum alerta neste nível.");
    else
        filtrados.ForEach(a => Console.WriteLine($"  {a}"));
}
