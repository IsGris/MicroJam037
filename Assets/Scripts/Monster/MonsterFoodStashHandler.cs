using UnityEngine;
using static FoodHelpers;

/// <summary>
/// Handle all food that goes into monster
/// </summary>
public class MonsterFoodStashHandler : FoodStashHandler
{
	protected override void HandleFoodCollect(Food food)
	{
		if (!IsFoodDish(food.type))
		{
			food.Despawn();
			SoundBehavior.Instance.PlaySound(Sound.MonsterEatBad);
			MonsterStats.Instance.OnBadFoodEat?.Invoke();
			return;
		}

		MonsterStats.Instance.AddFood(food.type, food.quantity);
		SoundBehavior.Instance.PlaySound(Sound.MonsterEatGood);
		food.Despawn();
	}
}
