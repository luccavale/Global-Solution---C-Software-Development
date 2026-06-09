namespace SatAlert.Models
{
    // Classe pública e abstrata: define o contrato de todo satélite
    public abstract class Satelite
    {
        
        public int Id { get; private set; }
        public string Nome { get; protected set; }
        public Coordenada Coordenada { get; protected set; }
        public DateTime DataLancamento { get; private set; }
        public bool Ativo { get; protected set; }

        // Contador estático
        private static int _contadorId = 1;

        protected Satelite(string nome, Coordenada coordenada)
        {
            Id = _contadorId++;
            Nome = nome;
            Coordenada = coordenada;
            DataLancamento = DateTime.Now;
            Ativo = true;
        }

        
        public abstract string ObterTipo();

        
        public virtual string ObterResumo()
        {
            return $"[{Id}] {Nome} | Tipo: {ObterTipo()} | {Coordenada} | Ativo: {Ativo} | Lançado: {DataLancamento:dd/MM/yyyy HH:mm}";
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }
}
