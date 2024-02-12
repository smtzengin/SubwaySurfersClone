using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms; 
    [SerializeField] private Transform spawnParent; 
    [SerializeField] private Transform playerTransform;

    private float spawnZ = 0f; 
    [SerializeField]private float platformLength = 210f;
    [SerializeField] private int safeZone = 140; 
    [SerializeField] private int numPlatformsToKeep = 3; 

    [SerializeField]private List<GameObject> activePlatforms = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < numPlatformsToKeep; i++)
        {
            if (i < 2) 
                SpawnPlatform(0);
            else
                SpawnPlatform(Random.Range(0, platforms.Count));
        }
    }

    private void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - numPlatformsToKeep * platformLength))
        {
            SpawnPlatform(Random.Range(0, platforms.Count));
            DeletePlatform();
        }
    }

    private void SpawnPlatform(int prefabIndex)
    {
        GameObject go = Instantiate(platforms[prefabIndex], Vector3.forward * spawnZ, Quaternion.identity, spawnParent);
        activePlatforms.Add(go);
        spawnZ += platformLength;
    }

    private void DeletePlatform()
    {
        Destroy(activePlatforms[0]);
        activePlatforms.RemoveAt(0);
    }
}
