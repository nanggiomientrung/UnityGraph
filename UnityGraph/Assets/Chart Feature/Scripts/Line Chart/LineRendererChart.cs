using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererChart : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LineRendererPointData nodeForDataPoint;

    public void CreateLineChart(Color lineColor, Vector2[] listPoint)
    {
        lineRenderer.positionCount = listPoint.Length;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        // đoạn này sẽ làm Bezier sau
        for (int i = 0; i < listPoint.Length; i++)
        {
            lineRenderer.SetPosition(i, listPoint[i]);

        }
    }
}

public enum LineRendererType
{
    HorizontalAxis = 0,
    VerticalAxis = 1,
    LineRendererChart = 2
}