using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class FoodStashHandler : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		var food = collision.gameObject.GetComponent<Food>();
		if (food == null)
			return;

		HandleFoodCollect(food);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		var food = collision.gameObject.GetComponent<Food>();
		if (food == null)
			return;

		HandleFoodDecollect(food);
	}

	protected abstract void HandleFoodCollect(Food food);
	protected virtual void HandleFoodDecollect(Food food) { }
}
