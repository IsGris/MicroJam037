using UnityEngine;
using TMPro;
using System;

public class FoodUIView : MonoBehaviour
{
	// INITIALIZE

    public FoodType type;
    public TextMeshProUGUI foodText;

	private void Start()
	{
		PlayerStats.Instance.OnFoodCountChage += Instance_OnFoodCountChage;
	}

	private void Instance_OnFoodCountChage(FoodType arg1, int arg2)
	{
		if (arg1 != type)
			return;

		UpdateText(arg2);
	}

	// VIEW METHODS

	protected void UpdateText(int NewText) =>
		foodText.text = Convert.ToString(NewText);
}
