using SatAlert.Models;

namespace SatAlert.Repositories
{
    
    // Esta parte cuida do armazenamento em memória
    public partial class AlertaRepository
    {
        private readonly List<Alerta> _alertas = new List<Alerta>();

        public void Adicionar(Alerta alerta)
        {
            if (alerta == null)
                throw new ArgumentNullException(nameof(alerta), "Alerta não pode ser nulo.");

            _alertas.Add(alerta);
        }

        public List<Alerta> ObterTodos()
        {
            return new List<Alerta>(_alertas);
        }

        public int ContarAlertas() => _alertas.Count;
    }
}
