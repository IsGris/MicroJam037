using UnityEngine;

public class GroundBushSpawner : MonoBehaviour
{
	private Bush bush;
	[SerializeField] private GameObject bushPrefab;

	public bool Spawn()
	{
		if (bush != null && bush.gameObject != null)
			return false;

		Instantiate(bushPrefab, transform.position, Quaternion.identity);
		return true;
	}
}
