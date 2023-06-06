using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSplineBezier : MonoBehaviour
{
    [SerializeField] private List<DrawCurveBezier> curvesBezier;
    [SerializeField] private CurveBezier typeOfCurvesBezier;

    public List<DrawCurveBezier> CurvesBeziers { get => curvesBezier; }
    public CurveBezier TypeOfCurvesBezier { get => typeOfCurvesBezier; }

    private void OnDrawGizmos()
    {
        int segmentsNumber = 100;
        Vector3 prevPoint = GetStartPoint();

        for (int i = 0; i < CurvesBeziers.Count; i++)
        {
            for (int j = 0; j < segmentsNumber; j++)
            {
                if (i > 0)
                {
                    GetPoint(i, 0).position = GetPoint(i - 1, 3).position;
                }
                float t = (float)j / segmentsNumber;
                Vector3 point = Vector3.zero;
                switch (TypeOfCurvesBezier)
                {
                    case CurveBezier.Cubic:
                        
                        point = Bezier.GetPointOnCubicCurveBezier(GetPoint(i, 0).position, GetPoint(i, 1).position,
                        GetPoint(i, 2).position, GetPoint(i, 3).position, t);
                        break;
                }
                Gizmos.DrawLine(prevPoint, point);
                prevPoint = point;
            }
        }
    }

    private Vector3 GetStartPoint()
    {
        return CurvesBeziers[0].transform.GetChild(0).position;
    }

    private Transform GetPoint(int curveIndex, int pointIndex)
    {
        return CurvesBeziers[curveIndex].transform.GetChild(pointIndex);
    }
}
