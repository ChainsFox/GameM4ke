using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint; //the position of our bowAttack object
    public GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        //flip the projectile's facing direction and movement based on the direction the character is facing at time of launch.
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.z, origScale.z); //*1 and *(-1)


    }
}
