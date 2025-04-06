using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// EVENTS

	/// <summary>
	/// Invokes when FoodCount of player stats change<br/><br/>
	/// 1st param - FoodType that is changed count<br/>
	/// 2nd param - New amount of food in this FoodType<br/>
	/// </summary>
	public event Action<FoodType, int> OnFoodCountChage;
	public event Action<int> OnGoldChange;
	public event Action<FoodType> OnReceiptUnlock;

	// INITIALIZE

	[SerializeField] private int moneyOnStart;
	[SerializeField] private int seedsOnStart;

	public static PlayerStats Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	private void Start()
	{
		Gold = moneyOnStart;
		AddFood(FoodType.Seed, seedsOnStart);
	}

	// RECEIPTS

	private List<FoodType> unlockedReceipts = new();

	public bool IsReceiptUnlocked(FoodType food) =>
		unlockedReceipts.Contains(food);

	public void UnlockReceipt(FoodType food)
	{
		if (!FoodHelpers.IsFoodDish(food) || unlockedReceipts.Contains(food))
			return;

		unlockedReceipts.Add(food);
		OnReceiptUnlock?.Invoke(food);
	}

	// GOLD

	public int Gold
	{
		get => _gold;
		set
		{
			_gold = Math.Max(0, value);
			OnGoldChange?.Invoke(_gold);
		}
	}

	private int _gold = 0;

	// FOOD

	private Dictionary<FoodType, int> foodCollected = new();
	
	public void SetFoodCount(FoodType type, int value)
	{
		foodCollected[type] = Math.Max(0, value);
		OnFoodCountChage.Invoke(type, foodCollected[type]);
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
}
