using UnityEngine;
using System.Collections;

public class DebugUI : MonoBehaviour
{
#if UNITY_EDITOR
	public float timeScaleMultiplier = 100f;

	public float defaultSkipDuration = 10.0f;

	private bool isSkipping = false;
	private float originalTimeScale;

	public void SkipTime(float secondsToSkip)
	{
		if (isSkipping || secondsToSkip <= 0)
			return;
		
		StartCoroutine(SkipTimeCoroutine(secondsToSkip));
	}

	public void SkipDefaultTime()
	{
		SkipTime(defaultSkipDuration);
	}

	private IEnumerator SkipTimeCoroutine(float duration)
	{
		isSkipping = true;
		originalTimeScale = Time.timeScale;

		Time.timeScale = timeScaleMultiplier;

		float timeElapsed = 0f;
		while (timeElapsed < duration)
		{
			timeElapsed += Time.deltaTime;

			yield return null;
		}

		Time.timeScale = originalTimeScale;
		isSkipping = false;
	}

	public void OnSkipButtonPress() =>
		SkipTime(defaultSkipDuration);


	public bool enableDebugGUI = true;
	public Rect guiButtonRect = new Rect(10, 10, 200, 30);

	void OnGUI()
	{
		if (!enableDebugGUI)
			return;

		GUI.depth = -1;

		string buttonText = $"Skip {defaultSkipDuration} seconds";
		if (isSkipping)
		{
			buttonText = "Skiping...";
			GUI.enabled = false;
		}

		if (GUI.Button(guiButtonRect, buttonText))
		{
			SkipDefaultTime();
		}

		GUI.enabled = true;
	}
#endif
}