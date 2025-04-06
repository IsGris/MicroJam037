using UnityEngine;
using TMPro;
using System;

public class MonsterNeedsUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Soup1Need;
	[SerializeField] private TextMeshProUGUI Soup2Need;
	[SerializeField] private TextMeshProUGUI Soup3Need;
    private MonsterStats monster;

	void Start()
    {
        monster = MonsterStats.Instance;
        monster.OnNeedsUpdate += UpdateNeeds;
    }

    void UpdateNeeds()
    {
        if (monster.Needs.ContainsKey(FoodType.Soup1))
            Soup1Need.text = Convert.ToString(monster.Needs[FoodType.Soup1]);
        else
			Soup1Need.text = "0";

		if (monster.Needs.ContainsKey(FoodType.Soup2))
			Soup2Need.text = Convert.ToString(monster.Needs[FoodType.Soup2]);
		else
			Soup2Need.text = "0";

		if (monster.Needs.ContainsKey(FoodType.Soup3))
			Soup3Need.text = Convert.ToString(monster.Needs[FoodType.Soup3]);
		else
			Soup3Need.text = "0";
	}
}
