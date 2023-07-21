using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSplineBezier : CurveBezier
{
    [SerializeField] private List<DrawCurveBezier> curvesBezier;
    public List<DrawCurveBezier> CurvesBeziers { get => curvesBezier; }

    private void Start()
    {
        bezierType = BezierType.Spline;
    }

    private void OnDrawGizmos()
    {
        int segmentsNumber = 100;
        Vector3 prevPoint = GetStartPoint();
        bezierType = BezierType.Spline;

        for (int i = 0; i < curvesBezier.Count; i++)
        {
            for (int j = 0; j < segmentsNumber; j++)
            {
                if (i > 0)
                {
                    GetPointFromCurve(i, 0).position = GetPointFromCurve(i - 1, 3).position;
                    GetPointFromCurve(i, 1).position = 2 * GetPointFromCurve(i - 1, 3).position - GetPointFromCurve(i - 1, 2).position;
                }
                float t = (float)j / segmentsNumber;
                Vector3 point = Vector3.zero;
                switch (curvesBezier[i].BezierType)
                {
                    case BezierType.Cubic:
                        
                        point = GetPointOnCubicCurveBezier(GetPointFromCurve(i, 0).position, GetPointFromCurve(i, 1).position,
                        GetPointFromCurve(i, 2).position, GetPointFromCurve(i, 3).position, t);
                        break;
                    default:
                        Debug.LogError($"In the spline bezier {gameObject.name} is some variant not cubic bezier type. Please, change all curves bezier on cubic.");
                        break;
                }
                Gizmos.DrawLine(prevPoint, point);
                prevPoint = point;
            }
        }
    }

    private Vector3 GetStartPoint()
    {
        return curvesBezier[0].transform.GetChild(0).position;
    }

    private Transform GetPointFromCurve(int curveIndex, int pointIndex)
    {
        return curvesBezier[curveIndex].transform.GetChild(pointIndex);
    }
}
