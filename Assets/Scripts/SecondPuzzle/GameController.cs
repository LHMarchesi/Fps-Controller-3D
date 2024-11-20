using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject ballPrefab;

    public int gridSize = 5;
    public float spacing = 3f;

    public Color[] ballColors;

    private List<Ball> balls = new List<Ball>();

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Color color = ballColors[Random.Range(0, ballColors.Length)];

                Vector3 position = new Vector3(i * spacing, 0, j * spacing);

                GameObject ballObj = Instantiate(ballPrefab, position, Quaternion.identity);

                Ball ball = ballObj.GetComponent<Ball>();
                ball.Setup(color, new Vector2(i, j));
                balls.Add(ball);
            }
        }
    }
}

