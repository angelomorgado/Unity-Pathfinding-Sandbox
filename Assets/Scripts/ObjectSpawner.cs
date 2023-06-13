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
        Debug.Log("FORA:" + spawnedObjects.Count);
        while (spawnedObjects.Count < maxObjects)
        {
            Debug.Log("DENTRO:" + spawnedObjects.Count);
            GameObject spawner = GetRandomSpawner();
            GameObject spawnedObject = Instantiate(objectToSpawn, spawner.transform.position, Quaternion.identity);
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