using System.Collections.Generic;
using UnityEngine;

public class DrawCurveBezier : MonoBehaviour
{
    [SerializeField] private CurveBezier typeOfCurveBezier;
    [SerializeField] private List<Transform> pointsOfCurve;
    public CurveBezier TypeOfCurveBezier { get => typeOfCurveBezier; }
    public List<Transform> PointsOfCurve { get => pointsOfCurve; }

    private void OnDrawGizmos()
    {
        int segmentsNumber = 100;
        Vector3 prevPoint = PointsOfCurve[0].position;

        for (int i = 0; i < segmentsNumber; i++)
        {
            float t = (float)i / segmentsNumber;
            Vector3 point = Vector3.zero;
            switch (typeOfCurveBezier)
            {
                case CurveBezier.Cubic:
                    point = Bezier.GetPointOnCubicCurveBezier(PointsOfCurve[0].position, PointsOfCurve[1].position,
                    PointsOfCurve[2].position, PointsOfCurve[3].position, t);
                    break;
                case CurveBezier.Quadratic:
                    point = Bezier.GetPointOnQuadraticCurveBezier(PointsOfCurve[0].position, PointsOfCurve[1].position,
                        PointsOfCurve[2].position, t);
                    break;
            }
            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }
    }
}

