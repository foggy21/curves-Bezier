using System.Collections.Generic;
using UnityEngine;

public class DrawCurveBezier : CurveBezier
{
    private void OnDrawGizmos()
    {
        int segmentsNumber = 100;
        Vector3 prevPoint = PointsOfCurve[0].position;

        for (int i = 0; i < segmentsNumber; i++)
        {
            float t = (float)i / segmentsNumber;
            Vector3 point = Vector3.zero;
            switch (bezierType)
            {
                case BezierType.Cubic:
                    point = GetPointOnCubicCurveBezier(PointsOfCurve[0].position, PointsOfCurve[1].position,
                    PointsOfCurve[2].position, PointsOfCurve[3].position, t);
                    break;
                case BezierType.Quadratic:
                    point = GetPointOnQuadraticCurveBezier(PointsOfCurve[0].position, PointsOfCurve[1].position,
                        PointsOfCurve[2].position, t);
                    break;
                default:
                    Debug.LogError($"Curve bezier {gameObject.name} has to have cubic or quadratic type");
                    break;
            }
            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }
    }

    
}

