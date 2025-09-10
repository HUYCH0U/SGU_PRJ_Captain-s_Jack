using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollow : MonoBehaviour
{
    [SerializeField] GameObject[] waypoint;
    private int CurrentWayPointIndex = 0;
    [SerializeField] private float speed = 2f;

    void Update()
    {
        if(Vector2.Distance(waypoint[CurrentWayPointIndex].transform.position,transform.position)<0.1f)
        {
            CurrentWayPointIndex++;
            if(CurrentWayPointIndex>=waypoint.Length)
            {
                CurrentWayPointIndex=0;
            }
        }
        transform.position= Vector2.MoveTowards(transform.position,waypoint[CurrentWayPointIndex].transform.position,Time.deltaTime*speed);
    }
}
