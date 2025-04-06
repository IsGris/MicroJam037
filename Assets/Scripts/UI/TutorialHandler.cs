using UnityEngine;
using UnityEngine.Events;

public class TutorialHandler : MonoBehaviour
{
    public UnityEvent OnReceiptBought;
    public UnityEvent OnSeedPlant;
    public UnityEvent OnCook;
    public UnityEvent OnMonsterEated;

	private bool IsReceiptBought = false;
	private bool IsSeedPlanted = false;
	private bool IsCooked = false;
	private bool IsMonsterEated = false;

	void Start()
    {
		PlayerStats.Instance.OnReceiptUnlock += Instance_OnReceiptUnlock;
		PlayerStats.Instance.OnFoodCountChage += Instance_OnFoodCountChage;
		CookingPotInventory.Instance.OnCook += Instance_OnCook;
		MonsterStats.Instance.OnFoodCountChage += Instance_OnFoodCountChage1;
	}


	private void OnDisable()
	{
		PlayerStats.Instance.OnReceiptUnlock -= Instance_OnReceiptUnlock;
		PlayerStats.Instance.OnFoodCountChage -= Instance_OnFoodCountChage;
		CookingPotInventory.Instance.OnCook -= Instance_OnCook;
		MonsterStats.Instance.OnFoodCountChage -= Instance_OnFoodCountChage1;
	}

	private void OnDestroy()
	{
		PlayerStats.Instance.OnReceiptUnlock -= Instance_OnReceiptUnlock;
		PlayerStats.Instance.OnFoodCountChage -= Instance_OnFoodCountChage;
		CookingPotInventory.Instance.OnCook -= Instance_OnCook;
		MonsterStats.Instance.OnFoodCountChage -= Instance_OnFoodCountChage1;
	}

	private void Instance_OnFoodCountChage1(FoodType arg1, int arg2)
	{
		if (IsReceiptBought && IsSeedPlanted && IsCooked && !IsMonsterEated)
		{
			IsMonsterEated = true;
			OnMonsterEated?.Invoke();
		}

	}

	private void Instance_OnCook()
	{
		if (IsReceiptBought && IsSeedPlanted && !IsCooked)
		{
			IsCooked = true;
			OnCook?.Invoke();
		}
	}

	private void Instance_OnFoodCountChage(FoodType arg1, int arg2)
	{
		if (arg1 == FoodType.Seed && !IsSeedPlanted && IsReceiptBought)
		{
			IsSeedPlanted = true;
			OnSeedPlant?.Invoke();
		}

	}

	private void Instance_OnReceiptUnlock(FoodType obj)
    {
		if (IsReceiptBought)
			return;

		IsReceiptBought = true;
		OnReceiptBought?.Invoke();
    }
}
