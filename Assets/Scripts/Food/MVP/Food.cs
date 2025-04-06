using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Contains which type is food
/// </summary>
/// <remarks>
/// If FoodType as short is >=1000 then it is dish
/// </remarks>
[Serializable]
public enum FoodType : short
{
	// Standart food
    Raw0 = 0,
	Raw1,
    Raw2,
    Raw3,

    // Seeds
    Seed = 250,

    // Dishes
    Soup1 = 1000,
    Soup2,
    Soup3
}

public class Food : MonoBehaviour
{
    public event Action OnCollected;

    public FoodType type;
    [Tooltip("How much player will recieve when took food")] public int quantity = 1;

    public void Collect()
    {
        PlayerStats.Instance.AddFood(type, quantity);
        OnCollected.Invoke();
    }

	public void Despawn()
	{
		OnCollected.Invoke();
	}
}
