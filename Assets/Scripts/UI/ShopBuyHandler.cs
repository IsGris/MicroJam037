using UnityEngine;

public class ShopBuyHandler : MonoBehaviour
{
	public void BuySoup1(int cost)
	{
		if (PlayerStats.Instance.Gold < cost || PlayerStats.Instance.IsReceiptUnlocked(FoodType.Soup1))
			return;

		PlayerStats.Instance.Gold -= cost;
		PlayerStats.Instance.UnlockReceipt(FoodType.Soup1);
		SoundBehavior.Instance.PlaySound(Sound.Buy);
	}

	public void BuySoup2(int cost)
	{
		if (PlayerStats.Instance.Gold < cost || PlayerStats.Instance.IsReceiptUnlocked(FoodType.Soup2))
			return;

		PlayerStats.Instance.Gold -= cost;
		PlayerStats.Instance.UnlockReceipt(FoodType.Soup2);
		SoundBehavior.Instance.PlaySound(Sound.Buy);
	}

	public void BuySoup3(int cost)
	{
		if (PlayerStats.Instance.Gold < cost || PlayerStats.Instance.IsReceiptUnlocked(FoodType.Soup3))
			return;

		PlayerStats.Instance.Gold -= cost;
		PlayerStats.Instance.UnlockReceipt(FoodType.Soup3);
		SoundBehavior.Instance.PlaySound(Sound.Buy);
	}

	public void BuySeed(int cost)
	{
		if (PlayerStats.Instance.Gold < cost)
			return;


		PlayerStats.Instance.Gold -= cost;
		PlayerStats.Instance.AddFood(FoodType.Seed, 1);
		SoundBehavior.Instance.PlaySound(Sound.Buy);
	}
}
