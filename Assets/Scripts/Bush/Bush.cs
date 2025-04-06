using UnityEngine;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class Bush : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdown;
    [SerializeField] private SpriteRenderer sprender;
	[SerializeField] private float TimeToReady;
	[SerializeField] private Color StartColor;
	[SerializeField] private Color EndColor;

	private float StartTime = float.NegativeInfinity;
	private float EndTime = float.NaN;
	private List<BushFoodSpawner> spawners = new List<BushFoodSpawner>();

	private void Awake()
	{
		spawners.AddRange(GetComponentsInChildren<BushFoodSpawner>());
		StartTime = Time.time;
		SoundBehavior.Instance.PlaySound(Sound.Plant);
	}

	private void Update()
	{
		if (!float.IsNaN(EndTime))
			return;
		float elapsed = StartTime + TimeToReady - Time.time;
		if (elapsed <= 0)
		{
			countdown.text = "READY";
			sprender.color = EndColor;
			EndTime = Time.time;
			SoundBehavior.Instance.PlaySound(Sound.PlantGrow);
			return;
		}
		Color currentColor = Color.Lerp(EndColor, StartColor, Mathf.Clamp01(elapsed / TimeToReady));

		sprender.color = currentColor;

		int minutes = Mathf.FloorToInt(elapsed / 60f);
		int seconds = Mathf.FloorToInt(elapsed % 60f);

		countdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	private void OnMouseDown()
	{
		if (!float.IsNaN(EndTime))
			Collect();
	}

	public void Collect()
	{
		foreach (var spawner in spawners)
			spawner.Spawn();

		SoundBehavior.Instance.PlaySound(Sound.BushCollect);
		Destroy(gameObject);
	}
}
