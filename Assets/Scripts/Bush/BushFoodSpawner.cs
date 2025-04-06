using UnityEngine;

public class BushFoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
	[SerializeField][Range(0, 1)] private float spawnChance;

	public void Spawn()
    {
        if (Random.Range(0f, 1f) <= spawnChance)
            Instantiate(foodPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
