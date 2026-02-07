using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    private Vector2 nextDirection = Vector2.up;
    private Vector2 currentDirection = Vector2.up;
    private List<Transform> segments = new List<Transform>();
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] private int initialLength = 4;

    public float moveInterval = 0.5f;
    private float moveTimer = 0.0f;

    private void Start()
    {
        ResetState();
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
            for (int i = segments.Count - 1; i > 0; i--) //Move the tail first and work up to the head
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
        if (input.x != 0 && input.y != 0) return; // Prevents angled movement by pressing two keys at once like A & W.

        if (input != -currentDirection && input != Vector2.zero)
        {
            nextDirection = input;
        }
    }

    private void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        transform.position = Vector3.zero;
        segments.Add(transform);

        for (int i = 1; i < initialLength; i++)
        {
            Grow();
        }

    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {

            ResetState();
        }
    }
}
