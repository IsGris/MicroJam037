using UnityEngine;

[RequireComponent(typeof(Food), typeof(FoodView))]
public class FoodPresenter : MonoBehaviour
{
    private Food model;
    private FoodView view;

	void Start()
    {
        model = GetComponent<Food>();
        view = GetComponent<FoodView>();
        model.OnCollected += view.DestroyGameObject;
        view.OnClicked += model.Collect;
    }
}
