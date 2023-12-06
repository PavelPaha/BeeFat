using BeeFat.Domain.Infrastructure;


namespace BeeFat.Data;

public interface IApplicationContext: IDisposable
{
    IEnumerable<ApplicationUser> BeeFatUsers { get; set; }
    IEnumerable<Food> Foods { get; set; }  
    IEnumerable<FoodProductGram> FoodProductsGrams { get; set; }
    IEnumerable<FoodProductPiece> FoodProductsPieces { get; set; }
}