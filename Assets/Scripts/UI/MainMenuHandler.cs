using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
	[SerializeField] private AudioSource mainMenuMusic;
	[SerializeField] private AudioSource SFXVolume;
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private Slider SFXVolumeSlider;

	private void Awake()
	{
		if (!PlayerPrefs.HasKey("MusicVolume"))
			PlayerPrefs.SetFloat("MusicVolume", 0.5f);
		if (!PlayerPrefs.HasKey("SFXVolume"))
			PlayerPrefs.SetFloat("SFXVolume", 0.5f);

		mainMenuMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
		musicVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MusicVolume"));
		musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
		SFXVolume.volume = PlayerPrefs.GetFloat("SFXVolume");
		SFXVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFXVolume"));
		SFXVolumeSlider.onValueChanged.AddListener(ChangeSFXVolume);
	}

	public void ChangeMusicVolume(float newVolume)
	{
		PlayerPrefs.SetFloat("MusicVolume", newVolume);
		mainMenuMusic.volume = newVolume;
	}

	public void ChangeSFXVolume(float newVolume)
	{
		PlayerPrefs.SetFloat("SFXVolume", newVolume);
		SFXVolume.volume = newVolume;
	}

	public void StartGame()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void GoMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
