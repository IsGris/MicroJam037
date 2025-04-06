using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMouthView : MonoBehaviour
{
	public event Action OnEatAnimationStart;
	public event Action OnEatAnimationEnd;

	public float delay;
	public SpriteRenderer spriteRenderer;
	public List<Sprite> mouthSprites;
	public GameObject OpenMouthGameObject;
	public GameObject ClosedMouthGameObject;

	private Coroutine currentAnimation;
	public void OpenMouth()
	{
		if (currentAnimation != null)
			StopCoroutine(currentAnimation);

		currentAnimation = StartCoroutine(PlayAnimation(mouthSprites));
	}

	public void CloseMouth()
	{
		if (currentAnimation != null)
			StopCoroutine(currentAnimation);

		List<Sprite> reversedList = new List<Sprite>(mouthSprites);
		reversedList.Reverse();
		currentAnimation = StartCoroutine(PlayAnimation(reversedList));
	}

	public void MakeEat()
	{
		if (currentAnimation != null)
			StopCoroutine(currentAnimation);

		currentAnimation = StartCoroutine(EatSequence());
	}

	private IEnumerator PlayAnimation(List<Sprite> sprites)
	{
		foreach (Sprite sprite in sprites)
		{
			spriteRenderer.sprite = sprite;
			yield return new WaitForSeconds(delay);
		}

		currentAnimation = null;
	}

	private IEnumerator EatSequence()
	{
		OnEatAnimationStart?.Invoke();
		ClosedMouthGameObject.SetActive(true);
		OpenMouthGameObject.SetActive(false);
		// Close mouth
		List<Sprite> reversedList = new List<Sprite>(mouthSprites);
		reversedList.Reverse();
		yield return StartCoroutine(PlayAnimation(reversedList));

		// Optional pause between close and open
		yield return new WaitForSeconds(delay);

		// Open mouth
		yield return StartCoroutine(PlayAnimation(mouthSprites));

		ClosedMouthGameObject.SetActive(false);
		OpenMouthGameObject.SetActive(true);
		currentAnimation = null;
		OnEatAnimationEnd?.Invoke();
	}
}
