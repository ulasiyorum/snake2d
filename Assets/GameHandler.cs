using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public Food food;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Press W-A-S-D or direction keys to start.";
        score.fontSize = 14;
        GameObject snakeHead = new GameObject("Snake");
        SpriteRenderer renderer = snakeHead.AddComponent<SpriteRenderer>();
        renderer.sprite = Instance.instance.snakeHead;
        snakeHead.AddComponent<Snake>();
        food = new Food(20, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (Snake.score >= 0)
        {
            score.text = "Score: " + Snake.score;
            score.fontSize = 25;
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
