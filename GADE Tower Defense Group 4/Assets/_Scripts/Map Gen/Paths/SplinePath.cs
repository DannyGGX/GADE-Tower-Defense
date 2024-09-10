using UnityEngine;
using UnityEngine.Splines;
using UnityUtils;

namespace DannyG.Paths
{
    [RequireComponent(typeof(SplineContainer))]
    public class SplinePath : MonoBehaviour
    {
        private SplineContainer splineContainer;
        
        
        public void GenerateSpline(Waypoint[] waypoints)
        {
            splineContainer = GetComponent<SplineContainer>();
            splineContainer.Spline.Clear();

            foreach (var waypoint in waypoints)
            {
                BezierKnot knot = new BezierKnot(waypoint.gameObject.transform.position.Add(y: 1));
                splineContainer.Spline.Add(knot);
            }
        }
    }
}