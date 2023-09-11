namespace WebApp.Models;

public class Supplier
{
    public long SupplierId { get; set; }    
    public string Name { get; set; }=String.Empty;
    public string City { get; set; } = String.Empty;
    public IEnumerable<Product>? Products { get; set; }
}
