using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    // Randomness parameters
    [Range(0f, 5f)] public float randomDeviationRadius = 2f; // Radius for random movement deviation
    public float randomDelayChance = 0.2f; // 20% chance to pause at a waypoint

    private bool isDelaying = false; // Tracks if the ghost is pausing randomly

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDelaying) return; // Skip update if pausing

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            // Random chance to delay movement
            if (Random.value < randomDelayChance)
            {
                StartCoroutine(RandomDelay());
                return;
            }

            // Select next waypoint with added randomness
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

            // Add random offset to the next waypoint position
            Vector3 randomOffset = Random.insideUnitSphere * randomDeviationRadius;
            randomOffset.y = 0; // Ensure offset remains on the same plane
            Vector3 randomDestination = waypoints[m_CurrentWaypointIndex].position + randomOffset;

            navMeshAgent.SetDestination(randomDestination);
        }
    }

    // Coroutine for random delay
    IEnumerator RandomDelay()
    {
        isDelaying = true;
        float delayTime = Random.Range(1f, 3f); // Random delay between 1 and 3 seconds
        yield return new WaitForSeconds(delayTime);
        isDelaying = false;
    }
}
