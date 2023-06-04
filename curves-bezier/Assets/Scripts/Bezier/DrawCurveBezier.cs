using System.Collections.Generic;
using UnityEngine;

public class DrawCurveBezier : MonoBehaviour
{
    [SerializeField] private CurveBezier typeOfCurveBezier;
    [SerializeField] private List<Transform> pointsOfCurve;

    public List<Transform> PointsOfCurve { get => pointsOfCurve; }

    private void OnDrawGizmos()
    {
        int segmentsNumber = 100;
        Vector3 prevPoint = PointsOfCurve[0].position;

        for (int i = 0; i < segmentsNumber; i++)
        {
            float t = (float)i / segmentsNumber;
            Vector3 point = Vector3.zero;
            if (typeOfCurveBezier == CurveBezier.Cubic)
            {
                point = Bezier.GetPointOnCubicCurveBezier(PointsOfCurve[0].position, PointsOfCurve[1].position,
                    PointsOfCurve[2].position, PointsOfCurve[3].position, t);
            }
            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }
    }


    private enum CurveBezier
    {
        Cubic,
        Square
    }
}
