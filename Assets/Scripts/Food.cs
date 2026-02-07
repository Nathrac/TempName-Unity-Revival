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
        float x, y;
        
        do
        {
            x = Random.Range(bounds.min.x, bounds.max.x);
            y = Random.Range(bounds.min.y, bounds.max.y);
            Collider[] intersect = CheckPosition(new Vector3(x,y,0.0f));

            if (intersect.Length == 0){
                break;
            }

        } while (true);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private Collider[] CheckPosition(Vector3 position)
    {
        return Physics.OverlapSphere(position, 0.01f);
    }
}
