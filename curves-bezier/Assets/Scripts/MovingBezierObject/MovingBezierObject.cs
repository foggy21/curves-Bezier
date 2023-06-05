using System.Collections.Generic;
using UnityEngine;

public class MovingBezierObject : MonoBehaviour
{
    [SerializeField] private GameObject curve;

    [SerializeField] private float speed;

    private List<Transform> pointsOfCurveBezier;
    private CurveBezier typeOfCurveBezier;

    private float t;

    public GameObject Curve { get => curve; set { } }

    private void Start()
    {
        pointsOfCurveBezier = GetPointsFromCurveBezier();
        typeOfCurveBezier = GetTypeOfCurveBezier();
    }
            
    private void Update()
    {
        t += speed * Time.deltaTime;
        Debug.Log(t + " " + Time.deltaTime);

        switch (typeOfCurveBezier)
        {
            case CurveBezier.Cubic:
                transform.position = Bezier.GetPointOnCubicCurveBezier(pointsOfCurveBezier[0].position, pointsOfCurveBezier[1].position,
            pointsOfCurveBezier[2].position, pointsOfCurveBezier[3].position, t);
                break;
            case CurveBezier.Quadratic:
                transform.position = Bezier.GetPointOnQuadraticCurveBezier(pointsOfCurveBezier[0].position, pointsOfCurveBezier[1].position,
            pointsOfCurveBezier[2].position, t);
                break;

        }   
    }

    private List<Transform> GetPointsFromCurveBezier()
    {
        return Curve.GetComponentInChildren<DrawCurveBezier>().PointsOfCurve;
    }

    private CurveBezier GetTypeOfCurveBezier()
    {
        return Curve.GetComponent<DrawCurveBezier>().TypeOfCurveBezier;
    }
}
