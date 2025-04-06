using UnityEngine;

public class ReceiptUnlockView : MonoBehaviour
{
    [SerializeField] private GameObject Soup1ReceiptBlocker;
    [SerializeField] private GameObject Soup2ReceiptBlocker;
    [SerializeField] private GameObject Soup3ReceiptBlocker;

	void Start()
    {
        PlayerStats.Instance.OnReceiptUnlock += UnlockReceipt;
    }

    void UnlockReceipt(FoodType receipt)
    {
        switch (receipt)
        {
            case FoodType.Soup1:
                Soup1ReceiptBlocker.SetActive(false);
                break;
            case FoodType.Soup2:
				Soup2ReceiptBlocker.SetActive(false);
				break;
            case FoodType.Soup3:
				Soup3ReceiptBlocker.SetActive(false);
				break;
        }
    }
}
