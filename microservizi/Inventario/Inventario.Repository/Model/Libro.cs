namespace Inventario.Repository.Model
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titolo { get; set; } = string.Empty;
        public string Trama { get; set; } = string.Empty;
        public string Autore { get; set; } = string.Empty;
        public decimal Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }
        public DateTime DataInserimento { get; set; }
        public int Fk_fornitore { get; set; }
        public Fornitore Fornitore { get; set; }
    }
}
