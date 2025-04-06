using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GroundSeedStashHandler : FoodStashHandler
{
	[SerializeField] private List<GroundBushSpawner> bushSpawners;

	private void Awake()
	{
		bushSpawners = bushSpawners
			.OrderBy(bush => Mathf.Abs(bush.transform.position.x - transform.position.x))
			.ToList();
	}

	protected override void HandleFoodCollect(Food food)
	{
		if (food.type != FoodType.Seed)
			return;

		foreach (var bush in bushSpawners)
		{
			if (bush.Spawn())
				break;
		}

		food.Despawn();
	}
}
