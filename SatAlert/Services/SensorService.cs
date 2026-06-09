using SatAlert.Models;

namespace SatAlert.Services
{
    // Implementação concreta: herda da abstrata e finaliza a lógica
    public class SensorService : SensorServiceBase
    {
        public override Alerta? RegistrarLeitura(Satelite satelite, double valorLeitura, string tipoLeitura)
        {
            if (!satelite.Ativo)
                throw new InvalidOperationException($"O satélite '{satelite.Nome}' está inativo e não pode registrar leituras.");

            double limite = 0;
            string descricao = "";

            // Polimorfismo via is/as para determinar limites por tipo
            if (satelite is SateliteClimatico climatico)
            {
                limite = climatico.LimiteTemperaturaCritica;
                descricao = $"Temperatura {valorLeitura}°C detectada (limite: {limite}°C)";
            }
            else if (satelite is SateliteAgricola agricola)
            {
                limite = agricola.LimiteUmidadeCritica;
                descricao = $"Umidade {valorLeitura}% detectada (limite: {limite}%)";
            }
            else
            {
                throw new ArgumentException("Tipo de satélite não reconhecido para leitura de sensor.");
            }

            var nivel = DefinirNivel(valorLeitura, limite);

            // Só gera alerta se nível for médio ou crítico
            if (nivel == NivelAlerta.Baixo)
            {
                Console.WriteLine($"\n  ✔ Leitura registrada em {DateTime.Now:HH:mm:ss}. Valores normais.");
                return null;
            }

            var alerta = new Alerta(descricao, nivel, satelite.Nome);
            _alertas.Add(alerta);

            Console.WriteLine($"\n  ⚠ {alerta}");
            return alerta;
        }
    }
}
