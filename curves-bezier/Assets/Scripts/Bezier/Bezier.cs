using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPointOnCubicCurveBezier(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 pointD, float t)
    {
        Vector3 pointAB = Vector3.Lerp(pointA, pointB, t);
        Vector3 pointBC = Vector3.Lerp(pointB, pointC, t);
        Vector3 pointCD = Vector3.Lerp(pointC, pointD, t);

        Vector3 pointABC = Vector3.Lerp(pointAB, pointBC, t);
        Vector3 pointBCD = Vector3.Lerp(pointBC, pointCD, t);

        Vector3 pointABCD = Vector3.Lerp(pointABC, pointBCD, t);

        return pointABCD;
    }

    public static Vector3 GetPointOnQuadraticCurveBezier(Vector3 pointA, Vector3 pointB, Vector3 pointC, float t)
    {
        Vector3 pointAB = Vector3.Lerp(pointA, pointB, t);
        Vector3 pointBC = Vector3.Lerp(pointB, pointC, t);

        Vector3 pointABC = Vector3.Lerp(pointAB, pointBC, t);

        return pointABC;
    }
}
