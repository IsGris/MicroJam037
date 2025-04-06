using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class MonsterStats : MonoBehaviour
{
	// EVENTS

	/// <summary>
	/// Invokes when FoodCount of player stats change<br/><br/>
	/// 1st param - FoodType that is changed count<br/>
	/// 2nd param - New amount of food in this FoodType<br/>
	/// </summary>
	public event Action<FoodType, int> OnFoodCountChage;
	public Action OnBadFoodEat;
	public Action OnNeedsUpdate;
	public event Action OnWaveClear;

	// INITIALIZE

	[SerializeField] TextMeshProUGUI deathTimer;
	[SerializeField] GameObject deathCanvas;
	public static MonsterStats Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		OnFoodCountChage += UpdateNeeds;
		OnNeedsUpdate += CheckWaveClear;
		OnWaveClear += ResetWaveVars;
	}

	private void Update()
	{
		if (float.IsNaN(StartWaveTime) || float.IsNaN(WaveTime) || deathCanvas.active == true)
			return;
		var timeLast = StartWaveTime + WaveTime - Time.time;
		if (timeLast <= 0)
		{
			deathTimer.text = "00:00";
			deathCanvas.SetActive(true);
			SoundBehavior.Instance.PlaySound(Sound.Death);
			return;
		}
		int minutes = Mathf.FloorToInt(timeLast / 60);
		int seconds = Mathf.FloorToInt(timeLast % 60);

		deathTimer.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
	}

	// HUNGER

	public Dictionary<FoodType, int> Needs;
	public Dictionary<FoodType, int> NeedsInCurrentWave;
	public float StartWaveTime = float.NaN;
	public float WaveTime = float.NaN;

	private void ResetWaveVars()
	{
		Needs.Clear();
		NeedsInCurrentWave.Clear();
		StartWaveTime = float.NaN;
		WaveTime = float.NaN;
		foodCollected.Clear();
	}

	private void UpdateNeeds(FoodType type, int amount)
	{
		if (!Needs.ContainsKey(type))
			return;
		Needs[type] = Math.Max(NeedsInCurrentWave[type] - amount, 0);
		OnNeedsUpdate?.Invoke();
	}

	private void CheckWaveClear()
	{
		foreach (var need in Needs)
		{
			if (need.Value != 0)
				return;
		}
		OnWaveClear?.Invoke();
	}

	// FOOD

	private Dictionary<FoodType, int> foodCollected = new();

	public void SetFoodCount(FoodType type, int value)
	{
		foodCollected[type] = Math.Max(0, value);
		OnFoodCountChage?.Invoke(type, foodCollected[type]);
	}

	public void AddFood(FoodType type, int value)
	{
		SetFoodCount(type, GetFoodCount(type) + value);
	}

	public void SubtractFood(FoodType type, int value) =>
		SetFoodCount(type, GetFoodCount(type) - value);

	public int GetFoodCount(FoodType type)
	{
		if (!foodCollected.ContainsKey(type))
			foodCollected[type] = 0;

		return foodCollected[type];
	}
}
