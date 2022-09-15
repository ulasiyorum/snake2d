using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float timer;
    public float delay;
    public Vector2Int gridPosition;
    public Vector2Int moveDirection;
    public int snakeSize;
    public static int score;
    public List<Vector2Int> snakeVectorList;
    public List<Quaternion> snakeRotationList;
    public List<GameObject> bodies;
    private void Awake()
    {
        score = -1;
        snakeSize = 1;
        snakeRotationList = new List<Quaternion>();
        bodies = new List<GameObject>();
        snakeVectorList = new List<Vector2Int>();
        gridPosition = new Vector2Int(10, 10);
    }
    private void Update()
    {
        SimpleMove();
        MovementHandler();
        if(snakeSize > 3 * Time.timeScale) { Time.timeScale *= 1.25f; }
    }


    void SimpleMove()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) )&& Instance.instance.direction != 3)
        {
            Instance.instance.direction = 2;
            moveDirection.y = 1;
            moveDirection.x = 0;
            if(score == -1) { score++; }
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && Instance.instance.direction != 2)
        {
            Instance.instance.direction = 3;
            moveDirection.y = -1;
            moveDirection.x = 0;
            if (score == -1) { score++; }
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && Instance.instance.direction != 4)
        {
            Instance.instance.direction = 1;
            moveDirection.y = 0;
            moveDirection.x = -1;
            if (score == -1) { score++; }
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && Instance.instance.direction != 1)
        {
            Instance.instance.direction = 4;
            moveDirection.y = 0;
            moveDirection.x = 1;
            if (score == -1) { score++; }
        }
    }
    void MovementHandler()
    {
        delay += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= .5f)
        {
            snakeRotationList.Insert(0, transform.rotation);
            snakeVectorList.Insert(0, gridPosition);
            gridPosition += moveDirection;
            if (snakeVectorList.Count > snakeSize)
            {
                snakeVectorList.RemoveAt(snakeVectorList.Count - 1);
            }
            if(snakeRotationList.Count > snakeSize)
            {
                snakeRotationList.RemoveAt(snakeRotationList.Count - 1);
            }

            for(int i=0; i<snakeVectorList.Count; i++)
            {
                Vector2Int snakeVector = snakeVectorList[i];
                if (bodies.Count > i) { Destroy(bodies[i]); bodies.RemoveAt(i); }
                bodies.Insert(i,new GameObject("body" + i,typeof(SpriteRenderer)));

                bodies[i].GetComponent<SpriteRenderer>().sprite = Instance.instance.snakeBody;
                bodies[i].transform.position = new Vector3(snakeVector.x, snakeVector.y);
                if ((new Vector3(gridPosition.x, gridPosition.y) == bodies[i].transform.position&&snakeSize>1))
                {
                    Instance.instance.GameOver();
                }
            }
            for(int i=0; i<snakeRotationList.Count; i++)
            {
                Quaternion snakeRotation = snakeRotationList[i];
                bodies[i].transform.rotation = snakeRotation;
            }
                
            timer = 0;
            
            transform.position = new Vector2(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, CalcAngle(Instance.instance.direction));
            Food.OnMove(gridPosition);
            
        }
    }

    private float CalcAngle(int direction) 
    {
        if(direction == 1) { return 90; }
        else if(direction == 4) { return -90; }
        else if(direction == 2) { return 0; }
        else if(direction == 3) { return 180; }
        return 1f;
    }
}
