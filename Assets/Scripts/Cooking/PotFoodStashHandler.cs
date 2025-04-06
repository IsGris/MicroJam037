using UnityEngine;

public class PotFoodStashHandler : FoodStashHandler
{
	protected override void HandleFoodCollect(Food food)
	{
		CookingPotInventory.Instance.AddFood(food.type, food.quantity);
		CookingPotInventory.Instance.foodGameObjects.Add(food.gameObject);
	}

	protected override void HandleFoodDecollect(Food food)
	{
		CookingPotInventory.Instance.SubtractFood(food.type, food.quantity);
		CookingPotInventory.Instance.foodGameObjects.Remove(food.gameObject);
	}
}
