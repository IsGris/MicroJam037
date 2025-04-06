using UnityEngine;

public class MonsterMouthViewPresenter : MonoBehaviour
{
    MonsterStats monster;
	[SerializeField] private MonsterMouthView monsterMouth;

    void Start()
    {
        monster = MonsterStats.Instance;
		monster.OnFoodCountChage += Monster_OnFoodCountChage;
		monster.OnBadFoodEat += monsterMouth.MakeEat;
	}

	private void Monster_OnFoodCountChage(FoodType arg1, int arg2) =>
        monsterMouth.MakeEat();
}
