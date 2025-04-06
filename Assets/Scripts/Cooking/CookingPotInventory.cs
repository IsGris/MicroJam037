using System.Collections.Generic;
using System;
using UnityEngine;

public class CookingPotInventory : MonoBehaviour
{
	// EVENTS

	/// <summary>
	/// Invokes when FoodCount of player stats change<br/><br/>
	/// 1st param - FoodType that is changed count<br/>
	/// 2nd param - New amount of food in this FoodType<br/>
	/// </summary>
	public event Action<FoodType, int> OnFoodCountChage;
	public event Action OnCook;

	// INITIALIZE

	public static CookingPotInventory Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	// FOOD

	public List<GameObject> foodGameObjects;
	private Dictionary<FoodType, int> foodCollected = new();
	[SerializeField] private Transform CookedSpawnPos;
	[SerializeField] private GameObject Soup1;
	[SerializeField] private GameObject Soup2;
	[SerializeField] private GameObject Soup3;

	public void SetFoodCount(FoodType type, int value)
	{
		foodCollected[type] = Math.Max(0, value);
		if (foodCollected[type] == 0)
		{
			foodCollected.Remove(type);
			OnFoodCountChage?.Invoke(type, 0);
		} else
			OnFoodCountChage?.Invoke(type, foodCollected[type]);
	}

	public void AddFood(FoodType type, int value) =>
		SetFoodCount(type, GetFoodCount(type) + value);

	public void SubtractFood(FoodType type, int value) =>
		SetFoodCount(type, GetFoodCount(type) - value);

	public int GetFoodCount(FoodType type)
	{
		if (!foodCollected.ContainsKey(type))
			foodCollected[type] = 0;

		return foodCollected[type];
	}

	// COOK

	public void Cook()
	{
		if (CheckSoupIngidients(FoodType.Soup1))
			Instantiate(Soup1, transform.position, Quaternion.identity);
		else if (CheckSoupIngidients(FoodType.Soup2))
			Instantiate(Soup2, transform.position, Quaternion.identity);
		else if (CheckSoupIngidients(FoodType.Soup3))
			Instantiate(Soup3, transform.position, Quaternion.identity);
		
		while (foodGameObjects.Count > 0)
		{
			Destroy(foodGameObjects[0]);
		}
		foodGameObjects.Clear();
		foodCollected.Clear();
		OnCook?.Invoke();
	}

	private bool CheckSoupIngidients(FoodType soupType)
	{
		switch (soupType)
		{
			case FoodType.Soup1:
				if (foodCollected.Count == 2
					&& foodCollected.ContainsKey(FoodType.Raw0)
					&& foodCollected.ContainsKey(FoodType.Raw1)

					&& foodCollected[FoodType.Raw0] == 2
					&& foodCollected[FoodType.Raw1] == 1
					)
					return true;
				break;
			case FoodType.Soup2:
				if (foodCollected.Count == 3
					&& foodCollected.ContainsKey(FoodType.Raw0)
					&& foodCollected.ContainsKey(FoodType.Raw1)
					&& foodCollected.ContainsKey(FoodType.Raw2)

					&& foodCollected[FoodType.Raw0] == 1
					&& foodCollected[FoodType.Raw1] == 2
					&& foodCollected[FoodType.Raw2] == 1
					)
					return true;
				break;
			case FoodType.Soup3:
				if (foodCollected.Count == 4
					&& foodCollected.ContainsKey(FoodType.Raw0)
					&& foodCollected.ContainsKey(FoodType.Raw1)
					&& foodCollected.ContainsKey(FoodType.Raw2)
					&& foodCollected.ContainsKey(FoodType.Raw3)

					&& foodCollected[FoodType.Raw0] == 3
					&& foodCollected[FoodType.Raw1] == 2
					&& foodCollected[FoodType.Raw2] == 1
					&& foodCollected[FoodType.Raw3] == 1
					)
					return true;
				break;
		}
		return false;
	}
}
