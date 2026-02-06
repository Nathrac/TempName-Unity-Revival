using System;
using UnityEngine;

public class FoodDisable : MonoBehaviour
{
    public static event Action foodEaten;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foodEaten?.Invoke();
        }
    }

}
