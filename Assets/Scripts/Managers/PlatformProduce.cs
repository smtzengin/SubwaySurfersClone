using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProduce : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnTransforms;
    [SerializeField] private List<GameObject> spawnObjects;    
    private Dictionary<int, int> spawnedObjectIndices = new Dictionary<int, int>();

    private void Start()
    {
        for (int i = 0; i < spawnTransforms.Count; i++)
        {
            int randomObject = GetRandomObjectIndex(i);
            Instantiate(spawnObjects[randomObject], spawnTransforms[i]);
            spawnedObjectIndices.Add(i, randomObject);
        }
    }
    private int GetRandomObjectIndex(int currentIndex)
    {
        int maxAttempts = spawnObjects.Count; 
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            int randomObject = Random.Range(0, spawnObjects.Count);
            if (CheckValidity(currentIndex, randomObject))
            {
                return randomObject;
            }
        }        
        return Random.Range(0, spawnObjects.Count); 
    }

    private bool CheckValidity(int currentIndex, int objectIndex)
    {        
        int rowStartIndex = currentIndex - currentIndex % 9; 
        for (int i = rowStartIndex; i < currentIndex; i++)
        {
            if (spawnedObjectIndices.ContainsKey(i) && spawnedObjectIndices[i] == objectIndex)
                return false;
        }
        
        int[] verticalIndices = { currentIndex % 9, currentIndex % 9 + 9, currentIndex % 9 + 18 };
        foreach (int i in verticalIndices)
        {
            if (i != currentIndex && spawnedObjectIndices.ContainsKey(i) && spawnedObjectIndices[i] == objectIndex)
                return false;
        }

        return true; 
    }
}
