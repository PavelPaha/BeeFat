namespace BeeFat.Tests;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Bgu { get; set; }
    public string Kcal { get; set; }
}

public class DayItem
{
    public string Идентификатор { get; set; }
    public List<MealItem> Завтрак { get; set; }
    public List<MealItem> Обед { get; set; }
    public List<MealItem> Перекус { get; set; }
    public List<MealItem> Ужин { get; set; }
}

public class MealItem
{
    public int Продукт { get; set; }
    public int? Граммовка { get; set; }
    public int? Количество { get; set; }
}