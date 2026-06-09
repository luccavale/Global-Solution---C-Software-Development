using SatAlert.Models;

namespace SatAlert.Repositories
{
    // Classe partial: segunda parte com métodos de consulta/filtro
    public partial class AlertaRepository
    {
        public List<Alerta> ObterPorNivel(NivelAlerta nivel)
        {
            return _alertas.FindAll(a => a.Nivel == nivel);
        }

        public List<Alerta> ObterPorSatelite(string nomeSatelite)
        {
            return _alertas.FindAll(a => a.NomeSatelite.Equals(nomeSatelite, StringComparison.OrdinalIgnoreCase));
        }

        public Alerta? ObterMaisRecente()
        {
            return _alertas.Count > 0 ? _alertas[^1] : null;
        }
    }
}
