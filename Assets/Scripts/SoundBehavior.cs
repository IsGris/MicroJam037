using System;
using UnityEngine;

[Serializable]
public enum Sound
{
    Buy,
    BTNClick,
    BushCollect,
    FoodCollect,
    Cook,
    Death,
    MonsterEatBad,
    MonsterEatGood,
    Plant,
    PlantGrow
}

public class SoundBehavior : MonoBehaviour
{
	public static SoundBehavior Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	[SerializeField] private AudioSource SFXsource;
    
    [SerializeField] private AudioClip buy;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip bushcollect;
    [SerializeField] private AudioClip foodcollect;
    [SerializeField] private AudioClip cook;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip monstereatbad;
    [SerializeField] private AudioClip monstereatgood;
    [SerializeField] private AudioClip plant;
    [SerializeField] private AudioClip plantgrow;

    public void PlayClick() =>
        PlaySound(Sound.BTNClick);

    public void PlayCook() =>
        PlaySound(Sound.Cook);

    public void PlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.Buy:
                playSFX(buy);
                break;
            case Sound.BTNClick:
                playSFX(click);
                break;
			case Sound.BushCollect:
                playSFX(bushcollect);
                break;
			case Sound.FoodCollect:
                playSFX(foodcollect);
                break;
			case Sound.Cook:
                playSFX(cook);
                break;
			case Sound.Death:
                playSFX(death);
                break;
			case Sound.MonsterEatBad:
                playSFX(monstereatbad);
                break;
			case Sound.MonsterEatGood:
                playSFX(monstereatgood);
                break;
			case Sound.Plant:
                playSFX(plant);
                break;
			case Sound.PlantGrow:
                playSFX(plantgrow);
                break;
		}
    }

	private void playSFX (AudioClip clip) =>
        SFXsource.PlayOneShot(clip);
}
