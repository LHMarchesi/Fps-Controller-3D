using UnityEngine;

public class Ball : MonoBehaviour
{
    public Color color;

    public Vector2 gridPosition;

    private Renderer rend;

    public bool isConnected = false;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void Setup(Color _color, Vector2 _gridPosition)
    {
        color = _color;

        gridPosition = _gridPosition;

        rend.material.color = color;
    }
}


