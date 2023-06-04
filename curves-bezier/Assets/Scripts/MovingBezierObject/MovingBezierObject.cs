using System.Collections.Generic;
using UnityEngine;

public class MovingBezierObject : MonoBehaviour
{
    [SerializeField] private GameObject curveBezier;

    [SerializeField] private float speed;

    private List<Transform> pointsOfCurveBezier;

    private float t;

    public GameObject CurveBezier { get => curveBezier; set { } }

    private void Start()
    {
        pointsOfCurveBezier = GetPointsFromCurveBezier();
    }
            
    private void Update()
    {
        t += speed * Time.deltaTime;
        Debug.Log(t + " " + Time.deltaTime);
        transform.position = Bezier.GetPointOnCubicCurveBezier(pointsOfCurveBezier[0].position, pointsOfCurveBezier[1].position,
            pointsOfCurveBezier[2].position, pointsOfCurveBezier[3].position, t);
        
    }

    private List<Transform> GetPointsFromCurveBezier()
    {
        return CurveBezier.GetComponentInChildren<DrawCurveBezier>().PointsOfCurve;
    }
}
