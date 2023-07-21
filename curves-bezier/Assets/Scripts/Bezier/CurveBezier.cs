using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveBezier : Bezier
{
    [SerializeField] protected BezierType bezierType;

    public BezierType BezierType { get => bezierType; }

    [SerializeField] private List<Transform> pointsOfCurve;
    public List<Transform> PointsOfCurve { get => pointsOfCurve; }
}
