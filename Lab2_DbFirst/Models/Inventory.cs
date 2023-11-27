namespace Lab2_DbFirst.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? StoreId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public int? StockQuantity { get; set; }

    public virtual Book Isbn13Navigation { get; set; } = null!;

    public virtual Store? Store { get; set; }
}
