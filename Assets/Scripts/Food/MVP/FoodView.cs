using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FoodView : MonoBehaviour
{
	public event Action OnClicked;

	[SerializeField] private float clickMaxDistanceDelta = 10f;
	[SerializeField] private float clickMaxDuration = 0.5f;

	private Camera mainCamera;
	private Rigidbody2D rb;
	private Vector3 initialMousePosition;
	private float mouseDownTime;
	private bool isPotentiallyDragging;
	private bool isActuallyDragging;
	private bool isInteracting;
	private Vector3 offset;

	void Awake()
	{
		mainCamera = Camera.main;
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			bool hitOurCollider = false;

			Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);

			foreach (RaycastHit2D hit in hits)
			{
				if (hit.collider != null && hit.collider.gameObject == this.gameObject)
				{
					hitOurCollider = true;
					isInteracting = true;

					isPotentiallyDragging = true;
					isActuallyDragging = false;
					mouseDownTime = Time.time;
					initialMousePosition = Input.mousePosition;

					if (rb != null)
						rb.simulated = false;

					Vector3 mouseWorldPosWithZ = mainCamera.ScreenToWorldPoint(new Vector3(
						Input.mousePosition.x,
						Input.mousePosition.y,
						mainCamera.WorldToScreenPoint(transform.position).z
					));
					offset = transform.position - mouseWorldPosWithZ;
					break;
				}
			}

			if (!hitOurCollider)
			{
				isInteracting = false;
			}
		}

		if (isInteracting && Input.GetMouseButton(0))
		{
			if (!isPotentiallyDragging)
				return;

			if (!isActuallyDragging)
			{
				float distanceFromStart = Vector3.Distance(Input.mousePosition, initialMousePosition);
				if (distanceFromStart > clickMaxDistanceDelta)
				{
					isActuallyDragging = true;
				}
			}

			if (isActuallyDragging)
			{
				Vector3 currentScreenPoint = new Vector3(
					Input.mousePosition.x,
					Input.mousePosition.y,
					mainCamera.WorldToScreenPoint(transform.position).z
				);
				Vector3 currentWorldPos = mainCamera.ScreenToWorldPoint(currentScreenPoint) + offset;
				transform.position = currentWorldPos;
			}
		}

		if (isInteracting && Input.GetMouseButtonUp(0))
		{
			if (rb != null)
				rb.simulated = true;

			if (!isPotentiallyDragging)
			{
				isInteracting = false;
				return;
			}


			if (!isActuallyDragging)
			{
				float mouseUpTime = Time.time;
				float pressDuration = mouseUpTime - mouseDownTime;
				float distance = Vector3.Distance(Input.mousePosition, initialMousePosition);

				if (pressDuration <= clickMaxDuration && distance <= clickMaxDistanceDelta)
				{
					OnClicked?.Invoke();
					SoundBehavior.Instance.PlaySound(Sound.FoodCollect);
				}
			}

			isPotentiallyDragging = false;
			isActuallyDragging = false;
			isInteracting = false;
		}
	}

	public void DestroyGameObject()
	{
		Destroy(gameObject);
	}
}