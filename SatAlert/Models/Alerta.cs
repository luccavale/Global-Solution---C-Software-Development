namespace SatAlert.Models
{
    public enum NivelAlerta
    {
        Baixo,
        Medio,
        Critico
    }

    public class Alerta
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public NivelAlerta Nivel { get; private set; }
        public DateTime GeradoEm { get; private set; }
        public string NomeSatelite { get; private set; }

        private static int _contadorId = 1;

        public Alerta(string descricao, NivelAlerta nivel, string nomeSatelite)
        {
            Id = _contadorId++;
            Descricao = descricao;
            Nivel = nivel;
            GeradoEm = DateTime.Now;
            NomeSatelite = nomeSatelite;
        }

        public override string ToString()
        {
            return $"[ALERTA #{Id}] {Nivel.ToString().ToUpper()} | {NomeSatelite} | {Descricao} | {GeradoEm:dd/MM/yyyy HH:mm:ss}";
        }
    }
}
