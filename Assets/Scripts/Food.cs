using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private BoxCollider2D gridArea;

    private void OnEnable()
    {
        FoodDisable.foodEaten += RandomPosition;
    }
    private void OnDisable()
    {
        FoodDisable.foodEaten -= RandomPosition;
    }

    private void Start()
    {
        RandomPosition();
    }

    private void RandomPosition()
    {
        Bounds bounds = gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
}
