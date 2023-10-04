using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateAxis : MonoBehaviour
{
    public float axisLength = 1f;
    public float axisThickness = 0.02f;
    void Start()
    {
        CreateAxisLine(Vector3.right, Color.red);
        CreateAxisLine(Vector3.up, Color.green); 
        CreateAxisLine(Vector3.forward, Color.blue);
    }

    private void CreateAxisLine(Vector3 direction, Color color)
    {
        GameObject axisObject = new GameObject("Axis");
        axisObject.transform.SetParent(transform);
        axisObject.transform.localPosition = Vector3.zero;
        LineRenderer lineRenderer = axisObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = axisThickness;
        lineRenderer.endWidth = axisThickness;
        lineRenderer.positionCount = 2;

        lineRenderer.useWorldSpace = false;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, direction * axisLength);

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    } 
}
