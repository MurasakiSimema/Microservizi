namespace Inventario.Shared
{
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Trama { get; set; }
        public string Autore { get; set; }
        public decimal Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }
        public int Fk_fornitore { get; set; }
    }
}
