namespace SatAlert.Models
{
    // Herda de Satelite
    public class SateliteAgricola : Satelite
    {
        public string RegiaoMonitorada { get; private set; }
        public double LimiteUmidadeCritica { get; private set; }

        public SateliteAgricola(string nome, Coordenada coordenada, string regiao, double limiteUmidade)
            : base(nome, coordenada)
        {
            RegiaoMonitorada = regiao;
            LimiteUmidadeCritica = limiteUmidade;
        }

        public override string ObterTipo() => "Agrícola";

        public override string ObterResumo()
        {
            return base.ObterResumo() + $" | Região: {RegiaoMonitorada} | Limite Umidade: {LimiteUmidadeCritica}%";
        }
    }
}
