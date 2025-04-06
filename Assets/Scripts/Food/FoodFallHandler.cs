using UnityEngine;

/// <summary>
/// Handles situtation when food goes out of playing field
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FoodFallHandler : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
    {
		var foodmodel = collision.gameObject.GetComponent<Food>();
		if (!foodmodel)
			return;

		foodmodel.Collect();
    }
}
