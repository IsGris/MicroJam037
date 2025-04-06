/// <summary>
/// Contains functions for food
/// </summary>
public static class FoodHelpers
{
	/// <summary>
	/// Check does food is dish
	/// </summary>
	/// <param name="food">food type of checking food</param>
	/// <returns>true if food is dish</returns>
	public static bool IsFoodDish(FoodType food) =>
		((short)food) >= 1000;
}
