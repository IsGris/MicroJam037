using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraViewSelector : MonoBehaviour
{

	// DEFAULT VARIABLES

	/// <summary>
	/// How much need to move camera in units to access right screen
	/// </summary>
	[SerializeField] public float ScreenWidth;
	/// <summary>
	/// How much need to move camera in units to access top screen
	/// </summary>
	[SerializeField] public float ScreenHeight;

	// VARIABLES

	private Camera mainCamera;

	// INITIALIZE

	public static CameraViewSelector Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		mainCamera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
			GetComponent<AudioSource>().Play();
	}

	// PUBLIC

	/// <summary>
	/// Available directions in which camera can be moved
	/// </summary>
	public enum Direction
	{
		Right,
		Left,
		Up,
		Down
	}

	/// <summary>
	/// Changes current screen by moving to given direction
	/// </summary>
	/// <param name="direction">direction in which camera needs to be moved</param>
	public void ChangeScreen(Direction direction)
	{
		switch (direction)
		{
			case Direction.Right:
				mainCamera.transform.position += new Vector3(ScreenWidth, 0, 0);
				break;
			case Direction.Left:
				mainCamera.transform.position += new Vector3(-ScreenWidth, 0, 0);
				break;
			case Direction.Up:
				mainCamera.transform.position += new Vector3(0, ScreenHeight, 0);
				break;
			case Direction.Down:
				mainCamera.transform.position += new Vector3(0, -ScreenHeight, 0);
				break;
		}
	}

	public void MoveScreenRight() =>
		ChangeScreen(Direction.Right);
	public void MoveScreenLeft() =>
		ChangeScreen(Direction.Left);
	public void MoveScreenUp() =>
		ChangeScreen(Direction.Up);
	public void MoveScreenDown() =>
		ChangeScreen(Direction.Down);
}
