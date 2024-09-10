using UnityEngine;
using UnityEngine.Splines;
using UnityUtils;

namespace DannyG.Paths
{
    [RequireComponent(typeof(SplineContainer))]
    public class SplinePath : MonoBehaviour
    {
        private SplineContainer splineContainer;
        
        
        public void GenerateSpline(Waypoint[] waypoints, GameObject splineArrow)
        {
            splineContainer = GetComponent<SplineContainer>();
            splineContainer.Spline.Clear();

            foreach (var waypoint in waypoints)
            {
                BezierKnot knot = new BezierKnot(waypoint.gameObject.transform.position.Add(y: 0.1f));
                splineContainer.Spline.Add(knot);
            }
            
            splineContainer.Spline.SetTangentMode(0, TangentMode.Linear);
            splineContainer.Spline.SetTangentMode(waypoints.Length - 1, TangentMode.Linear);

            Spline spline = splineContainer.Spline;
            int numberOfImages = waypoints.Length * 2;
            for (int i = 0; i < numberOfImages; i++)
            {
                float t = (float)i / (numberOfImages - 1);

                Vector3 position = spline.EvaluatePosition(t);
                Vector3 tangent = spline.EvaluateTangent(t);
                Quaternion rotation = Quaternion.LookRotation(tangent); // Align with the spline direction

                Instantiate(splineArrow, position, rotation, transform);
            }
        }
    }
}