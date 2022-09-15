using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    public Sprite snakeHead;
    public Sprite snakeBody;
    public Sprite food;
    public Transform background;
    public static Instance instance;
    public int direction;
    public GameObject gameOverScreen;
    // 1 -> saða 4-> sola 2-> yukarý 3-> aþaðý

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        Destroy(FindObjectOfType<Snake>());
        Destroy(Food.food);
        gameOverScreen.SetActive(true);
    }
}
