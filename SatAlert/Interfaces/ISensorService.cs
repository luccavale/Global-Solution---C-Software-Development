using SatAlert.Models;

namespace SatAlert.Interfaces
{
    // Garante desacoplamento e testabilidade (injeção de dependência)
    public interface ISensorService
    {
        Alerta? RegistrarLeitura(Satelite satelite, double valorLeitura, string tipoLeitura);
        List<Alerta> ObterAlertas();
        void ExibirHistorico();
    }
}
