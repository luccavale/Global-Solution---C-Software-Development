using SatAlert.Interfaces;
using SatAlert.Models;

namespace SatAlert.Services
{
    // Classe abstrata: implementa parte da interface, deixa o restante para subclasses
    public abstract class SensorServiceBase : ISensorService
    {
        // Lista protegida: acessível pelas subclasses
        protected List<Alerta> _alertas = new List<Alerta>();

        // Método abstrato que cada subclasse deve implementar
        public abstract Alerta? RegistrarLeitura(Satelite satelite, double valorLeitura, string tipoLeitura);

        public List<Alerta> ObterAlertas()
        {
            return _alertas;
        }

        public void ExibirHistorico()
        {
            if (_alertas.Count == 0)
            {
                Console.WriteLine("  Nenhum alerta registrado.");
                return;
            }

            foreach (var alerta in _alertas)
            {
                Console.WriteLine($"  {alerta}");
            }
        }

        // Método auxiliar protegido: reutilizável pelas subclasses
        protected NivelAlerta DefinirNivel(double valor, double limite)
        {
            double percentual = valor / limite;
            if (percentual >= 1.0) return NivelAlerta.Critico;
            if (percentual >= 0.8) return NivelAlerta.Medio;
            return NivelAlerta.Baixo;
        }
    }
}
