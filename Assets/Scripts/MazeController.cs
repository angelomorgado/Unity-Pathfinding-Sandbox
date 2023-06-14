using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.SceneManagement;

public class MazeController : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface surface;

    [SerializeField]
    GameObject blueWaypoint;

    [SerializeField]
    GameObject redWaypoint;

    [SerializeField]
    GameObject greenWaypoint;

    void Start()
    {
        blueWaypoint.transform.position = RandomNavmeshLocation();
        redWaypoint.transform.position = RandomNavmeshLocation();
        greenWaypoint.transform.position = RandomNavmeshLocation();
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

        if (Vector3.Distance(greenWaypoint.transform.position, GameObject.FindGameObjectWithTag("GreenZombie").transform.position) < 2.5f)
        {
            greenWaypoint.transform.position = RandomNavmeshLocation();
        }

        // Redirects user to main menu
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Vector3 RandomNavmeshLocation()
{
    UnityEngine.AI.NavMeshTriangulation navMeshData = UnityEngine.AI.NavMesh.CalculateTriangulation();
    int maxIndices = navMeshData.indices.Length - 3;

    while (true)
    {
        int randomIndex = Random.Range(0, maxIndices);
        Vector3 vertex1 = navMeshData.vertices[navMeshData.indices[randomIndex]];
        Vector3 vertex2 = navMeshData.vertices[navMeshData.indices[randomIndex + 1]];
        Vector3 vertex3 = navMeshData.vertices[navMeshData.indices[randomIndex + 2]];

        Vector3 centerPoint = (vertex1 + vertex2 + vertex3) / 3f;

        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(centerPoint, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
        {
            return hit.position;
        }
    }
}


}
