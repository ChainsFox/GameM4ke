using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints; //waypoints is the variable name, gameobject only because both of the waypoint are just gameobject without anything in them
    private int currentWaypointindex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
       if (Vector2.Distance(waypoints[currentWaypointindex].transform.position, transform.position) < .1f) //logic: if the current waypoint and the platform is < .1f we know that we are touching it, and we want to switch to the next waypoint
        {
            currentWaypointindex++;
            if (currentWaypointindex >= waypoints.Length)
            {
                currentWaypointindex = 0;
            }
        }
       transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointindex].transform.position, Time.deltaTime * speed); 
        /*speed is the game unit(2f mean 2 game unit = 1 rectangle in the tile map), 
         time.deltatime * speed => so that we move 2 unit per second, no matter the framerate of the device is.
         time.delta time is gonna be very common because you need it to make your game framerate independent.*/


    }
}
    