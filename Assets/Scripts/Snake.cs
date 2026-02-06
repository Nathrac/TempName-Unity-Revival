using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    private Vector2 nextDirection = Vector2.up;
    private Vector2 currentDirection = Vector2.up;
    private List<Transform> segments;
    [SerializeField] private Transform segmentPrefab;

    public float moveInterval = 0.5f;
    private float moveTimer = 0.0f;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(transform);
    }

    private void OnEnable()
    {
        FoodDisable.foodEaten += Grow;
    }

    private void OnDisable()
    {
        FoodDisable.foodEaten -= Grow;
    }

    private void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;

        if (moveTimer >= moveInterval)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }
            currentDirection = nextDirection;
            transform.position = new Vector3(
                Mathf.Round(transform.position.x) + currentDirection.x,
                Mathf.Round(transform.position.y) + currentDirection.y,
                0.0f
            );  
            moveTimer = 0f;
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input.x != 0 && input.y != 0) return;

        if (input != -currentDirection && input != Vector2.zero)
        {
            nextDirection = input;
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }
}
