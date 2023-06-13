using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> spawners;
    public GameObject objectToSpawn;
    public int maxObjects;
    public List<GameObject> spawnedObjects = new List<GameObject>();


    private void Update()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        while (spawnedObjects.Count < maxObjects)
        {
            GameObject spawner = GetRandomSpawner();
            Vector2 randomOffset = Random.insideUnitCircle.normalized * 5f;
            Vector3 spawnPosition = spawner.transform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
            Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            
            // Set the target of the spawned zombie
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            spawnedObject.GetComponent<Agent>().target = playerObject.transform;
            
            spawnedObjects.Add(spawnedObject);
        }
    }


    private GameObject GetRandomSpawner()
    {
        int randomIndex = Random.Range(0, spawners.Count);
        return spawners[randomIndex];
    }


    public void RemoveObject(GameObject objectToRemove)
    {
        spawnedObjects.Remove(objectToRemove);
    } 
}