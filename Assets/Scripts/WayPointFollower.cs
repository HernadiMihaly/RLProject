using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 3;

  
    void Update()
    {
        
        
            if (Vector3.Distance(transform.localPosition, waypoints[currentWaypointIndex].transform.localPosition) < .1f)
            {

                
            transform.Rotate(Vector3.up, -180f);
                
            currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }


            transform.localPosition = Vector3.MoveTowards(transform.localPosition, waypoints[currentWaypointIndex].transform.localPosition, speed * Time.deltaTime);

        

    }

   
}
