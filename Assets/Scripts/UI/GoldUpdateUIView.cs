using UnityEngine;
using TMPro;
using System;

public class GoldUpdateUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerStats.Instance.OnGoldChange += UpdateGoldAmount;
    }

    // Update is called once per frame
    void UpdateGoldAmount(int NewAmount)
    {
        goldText.text = Convert.ToString(NewAmount);
    }
}
