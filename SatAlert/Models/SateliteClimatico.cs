namespace SatAlert.Models
{
    // Herda de Satelite
    public class SateliteClimatico : Satelite
    {
        public double LimiteTemperaturaCritica { get; private set; }

        public SateliteClimatico(string nome, Coordenada coordenada, double limiteTemperatura)
            : base(nome, coordenada)
        {
            LimiteTemperaturaCritica = limiteTemperatura;
        }

        // Polimorfismo
        public override string ObterTipo() => "Climático";

        public override string ObterResumo()
        {
            return base.ObterResumo() + $" | Limite Temp: {LimiteTemperaturaCritica}°C";
        }
    }
}
