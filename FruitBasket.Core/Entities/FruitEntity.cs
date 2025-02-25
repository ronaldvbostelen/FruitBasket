namespace FruitBasket.Core.Interfaces;

public class FruitEntity : BaseEntity
{
    public string FruitType { get; set; } = "Type:Unknown";
    public DateTime PurchaseDate { get; set; }
}