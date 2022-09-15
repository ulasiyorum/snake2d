using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food {
    public static int width;
    public static int height;
    public static Vector2Int foodPosition;
    public static GameObject food;
    public static Snake snake;
    public Food(int a, int b)
    {
        width = a;
        height = b;
        snake = Object.FindObjectOfType<Snake>();
        SpawnFood();
    }


    public static void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (foodPosition == snake.gridPosition);
            
        food = new GameObject("Food", typeof(SpriteRenderer));
        food.GetComponent<SpriteRenderer>().sprite = Instance.instance.food;
        food.transform.position = new Vector3(foodPosition.x,foodPosition.y);
    }

    public static void OnMove(Vector2Int snakePosition)
    {
        if(snakePosition == foodPosition)
        {
            Object.Destroy(food);
            Snake.score += Random.Range(0, 8);
            if(Snake.score > snake.snakeSize * 4)
            {
                snake.snakeSize++;
            }
            SpawnFood();
        }
    }
}
