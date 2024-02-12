using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private List<GameObject> addedPlatforms;
    [SerializeField] private Vector3 position;
    [SerializeField] private Transform spawnParent;
    
    private void Awake()
    {
        GameObject currentPlatform = Instantiate(platforms[0], new Vector3(0, 0, 0), Quaternion.identity, spawnParent);
        addedPlatforms.Add(currentPlatform);
        position.z += 210;
    }


}
