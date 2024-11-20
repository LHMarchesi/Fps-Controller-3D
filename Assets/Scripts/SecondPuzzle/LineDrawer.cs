using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private List<Line> lines = new List<Line>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public void DrawLine(Ball ball1, Ball ball2)
    {
        Line newLine = new Line
        {
            startBall = ball1,
            endBall = ball2,
            startPosition = ball1.transform.position,
            endPosition = ball2.transform.position
        };

        lines.Add(newLine);

        lineRenderer.positionCount = lines.Count * 2;

        for (int i = 0; i < lines.Count; i++)
        {
            lineRenderer.SetPosition(i * 2, lines[i].startPosition);
            lineRenderer.SetPosition(i * 2 + 1, lines[i].endPosition);
        }
    }

    public void ClearLine()
    {
        lines.Clear();
        lineRenderer.positionCount = 0;
    }

    private class Line
    {
        public Ball startBall;
        public Ball endBall;

        public Vector3 startPosition;
        public Vector3 endPosition;
    }
}


