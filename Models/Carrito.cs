namespace frontendnet.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
