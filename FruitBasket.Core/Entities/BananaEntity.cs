namespace FruitBasket.Core.Interfaces;

public class BananaEntity : FruitEntity
{
    public BananaEntity()
    {
        FruitType = "Banana";
    }
    
    public bool IsOrganic { get; set; }
}