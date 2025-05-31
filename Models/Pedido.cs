namespace frontendnet.Models
{
    public class Pedido
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Subtotal { get; set; }
        public Producto Producto { get; set; }
    }
}