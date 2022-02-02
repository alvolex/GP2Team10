using System;
using UnityEngine;


public class BezierUtility : MonoBehaviour
{
    [Header("Should have exactly 4 control points")]
    [SerializeField] private Transform[] controlPoints;
    
    [SerializeField] private bool shouldUseAnimationCurve;
    [SerializeField] private AnimationCurve animCurve;

    private Vector3 gizmoPos;
    
    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1.05f; t+=0.05f)
        {
            gizmoPos = QuadraticBezier(t);
            Gizmos.DrawSphere(gizmoPos, 0.25f);
        }
        
        Gizmos.DrawLine(
            new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, controlPoints[0].position.z),
            new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, controlPoints[1].position.z));
        Gizmos.DrawLine(
            new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, controlPoints[2].position.z),
            new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, controlPoints[3].position.z));
    }


    public Vector3 QuadraticBezier(float t)
    {
        if (shouldUseAnimationCurve)
        {
            t = animCurve.Evaluate(t);
            return Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                   3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                   3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                   Mathf.Pow(t, 3) * controlPoints[3].position;
        }
        
        return Mathf.Pow(1 - t, 3) * controlPoints[0].position +
               3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
               3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
               Mathf.Pow(t, 3) * controlPoints[3].position;
    }

}
