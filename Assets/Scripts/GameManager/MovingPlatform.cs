using Unity.AI.Navigation;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] waypoints;
    public float speed = 0.1f;
    private bool isActive = false;

    private int currentWaypointIndex = 0;
    private bool isMovingForward = true;
    
    public NavMeshLink navMeshLinkIn;
    public NavMeshLink navMeshLinkOut;
    
    private void Update()
    {
        if (isActive)
        {
            MovePlatform();
        }
        
        UpdateNavMeshLinks();
    }
    
    public void Activate()
    {
        if (!isActive) // Active la plateforme seulement si elle n'est pas déjà active
        {
            isActive = true;
            isMovingForward = currentWaypointIndex == 0; // Détermine la direction de départ
        }
    }

    void MovePlatform()
    {
        if (waypoints.Length == 0) return;
        
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], speed * Time.deltaTime);
        
        if (transform.position == waypoints[currentWaypointIndex])
        {
            if (isMovingForward && currentWaypointIndex == waypoints.Length - 1)
            {
                isMovingForward = false;
                isActive = false; // Désactive la plateforme une fois qu'elle atteint le dernier waypoint
            }
            else if (!isMovingForward && currentWaypointIndex == 0)
            {
                isActive = false; // Désactive également dans ce cas
            }

            // Calcule le prochain waypoint index
            currentWaypointIndex += isMovingForward ? 1 : -1;
        }
    }
    
    void UpdateNavMeshLinks()
    {
        if (transform.position == waypoints[0])
        {
            navMeshLinkIn.enabled = true;
        }
        else
        {
            navMeshLinkIn.enabled = false;
        }
        
        if (transform.position == waypoints[1])
        {
            navMeshLinkOut.enabled = true;
        }
        else
        {
            navMeshLinkOut.enabled = false;
        }
    }
}