using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// If user drags current GameObject, it will spawn food from inventory at mouse position
/// </summary>
public class FoodDragAndSpawn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public FoodType type;
	public GameObject prefab;

	private GameObject spawnedObject;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (PlayerStats.Instance.GetFoodCount(type) <= 0)
			return;

		PlayerStats.Instance.SubtractFood(type, 1);
		Vector3 spawnPosition = GetMouseWorldPosition();
		spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
		spawnedObject.GetComponent<Rigidbody2D>().simulated = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (spawnedObject != null)
			spawnedObject.transform.position = GetMouseWorldPosition();
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (spawnedObject == null)
			return;
		spawnedObject.GetComponent<Rigidbody2D>().simulated = true;
		spawnedObject = null;
	}

	private Vector3 GetMouseWorldPosition()
	{
		Vector3 screenPosition = Input.mousePosition;
		screenPosition.z = 10f;
		return Camera.main.ScreenToWorldPoint(screenPosition);
	}
}
