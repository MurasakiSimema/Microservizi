namespace Inventario.Repository.Model
{
    public class Fornitore
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Indirizzo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Libro> Libri { get; set; }
    }
}
