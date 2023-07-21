using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingBezierObject : Bezier
{
    [SerializeField] private GameObject curve;

    [SerializeField] private float speed;

    private List<Transform> pointsOfCurveBezier;
    private List<List<Transform>> pointsOfSplineBezier;
    private CurveBezier curveBezier;
    private bool moveForward = true;
    private float t = 0;
    private int splineIndex = 0;

    public GameObject Curve { get => curve; }

    private void Start()
    {
        curveBezier = curve.GetComponent<CurveBezier>();
        if (curveBezier != null &&
            (curveBezier.BezierType == BezierType.Cubic || curveBezier.BezierType == BezierType.Quadratic))
        {
            pointsOfCurveBezier = curveBezier.PointsOfCurve;
        }
        else if (curveBezier != null && curveBezier.BezierType == BezierType.Spline)
        {
            pointsOfSplineBezier = GetPointsOfSplineBezier();
        }
    }
            
    private void Update()
    {
        if (curveBezier != null)
        {
            switch (curveBezier.BezierType)
            {
                case BezierType.Cubic:
                    MoveAlongCubicCurveBezier();
                    break;
                case BezierType.Quadratic:
                    MoveAlongQuadraticCurveBezier();
                    break;
                case BezierType.Spline:
                    MoveAlongSplineBezier();
                    break;
            }
        }
    }

    private void MoveAlongCubicCurveBezier()
    {
        if (moveForward)
        {
            transform.position = GetPointOnCubicCurveBezier(pointsOfCurveBezier[0].position, pointsOfCurveBezier[1].position,
        pointsOfCurveBezier[2].position, pointsOfCurveBezier[3].position, t);
            t += speed * Time.deltaTime;
            if (t >= 1)
            {
                moveForward = false;
            }
        }
        else
        {
            transform.position = GetPointOnCubicCurveBezier(pointsOfCurveBezier[0].position, pointsOfCurveBezier[1].position,
        pointsOfCurveBezier[2].position, pointsOfCurveBezier[3].position, t);
            t -= speed * Time.deltaTime;
            if (t <= 0)
            {
                moveForward = true;
            }
        }
    }

    private void MoveAlongQuadraticCurveBezier()
    {
        if (moveForward)
        {
            transform.position = GetPointOnQuadraticCurveBezier(pointsOfCurveBezier[0].position, pointsOfCurveBezier[1].position,
        pointsOfCurveBezier[2].position, t);
            t += speed * Time.deltaTime;
            if (t >= 1)
            {
                moveForward = false;
            }
        }
        else
        {
            transform.position = GetPointOnQuadraticCurveBezier(pointsOfCurveBezier[0].position, pointsOfCurveBezier[1].position,
        pointsOfCurveBezier[2].position, t);
            t -= speed * Time.deltaTime;
            if (t <= 0)
            {
                moveForward = true;
            }
        }
    }

    private void MoveAlongSplineBezier()
    {
        if (moveForward)
        {
            transform.position = GetPointOnCubicCurveBezier(pointsOfSplineBezier[splineIndex][0].position, pointsOfSplineBezier[splineIndex][1].position,
                pointsOfSplineBezier[splineIndex][2].position, pointsOfSplineBezier[splineIndex][3].position, t);
            t += speed * Time.deltaTime;
            if (t >= 1 && (splineIndex + 1) != pointsOfSplineBezier.Count)
            {
                t = 0;
                splineIndex++;
            } 
            else if (t >= 1 && (splineIndex + 1) == pointsOfSplineBezier.Count)
            {
                t = 1;
                moveForward = false;
            }
        }
        else 
        {
            transform.position = GetPointOnCubicCurveBezier(pointsOfSplineBezier[splineIndex][0].position, pointsOfSplineBezier[splineIndex][1].position,
                pointsOfSplineBezier[splineIndex][2].position, pointsOfSplineBezier[splineIndex][3].position, t);
            t -= speed * Time.deltaTime;
            if (t <= 0 && splineIndex != 0)
            {
                t = 1;
                splineIndex--;
            }
            else if (t <= 0 && splineIndex == 0)
            {
                t = 0;
                moveForward = true;
            }
        }
    }

    private List<List<Transform>> GetPointsOfSplineBezier()
    {
        List<List<Transform>> pointsOfSplineBezier = new List<List<Transform>>();
        List<DrawCurveBezier> curvesBezier = curveBezier.GetComponentsInChildren<DrawCurveBezier>().ToList();
        foreach (var curve in curvesBezier)
        {
            if (curve.BezierType == BezierType.Cubic)
            {
                pointsOfSplineBezier.Add(curve.PointsOfCurve);
            }
            else
            {
                Debug.LogError($"In spline bezier {gameObject.name} exists not cubic curve bezier. Please, change all curve bezier on cubic.");
            }
        }
        return pointsOfSplineBezier;
    }
}
