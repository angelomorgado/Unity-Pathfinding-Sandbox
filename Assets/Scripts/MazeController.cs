using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class MazeController : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface surface;

    [SerializeField]
    GameObject blueWaypoint;

    [SerializeField]
    GameObject redWaypoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the waypoints collide with the agents, move them to new positions
        if (Vector3.Distance(blueWaypoint.transform.position, GameObject.FindGameObjectWithTag("BlueZombie").transform.position) < 2.5f)
        {
            blueWaypoint.transform.position = RandomNavmeshLocation();
        }

        if (Vector3.Distance(redWaypoint.transform.position, GameObject.FindGameObjectWithTag("RedZombie").transform.position) < 2.5f)
        {
            redWaypoint.transform.position = RandomNavmeshLocation();
        }
    }

    Vector3 RandomNavmeshLocation()
    {
        UnityEngine.AI.NavMeshTriangulation navMeshData = UnityEngine.AI.NavMesh.CalculateTriangulation();
        int maxIndices = navMeshData.indices.Length - 3;

        while (true)
        {
            int randomIndex = Random.Range(0, maxIndices);
            Vector3 randomPoint = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[randomIndex]], navMeshData.vertices[navMeshData.indices[randomIndex + 1]], Random.value);
            randomPoint = Vector3.Lerp(randomPoint, navMeshData.vertices[navMeshData.indices[randomIndex + 2]], Random.value);

            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
            {
                Vector3 direction = hit.position - randomPoint;
                Vector3 centerPoint = randomPoint + direction.normalized * 0.5f;
                return centerPoint;
            }
        }
    }

}
