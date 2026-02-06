using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Handle food spawning. when gets eaten, spawn new food in random location that does not include the snake.  

    private void OnEnable()
    {
        FoodDisable.foodEaten += InstatiateFood;
    }

    private void OnDisable()
    {
        FoodDisable.foodEaten -= InstatiateFood;
    }

    private void InstatiateFood()
    {
        
    }
}
