using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainPlatform : MonoBehaviour
{
    [SerializeField] private GameObject trainObject;
    [SerializeField] private Transform trainFinishTransform;
    public void ActivateTrain()
    {
        trainObject.transform.DOMoveZ(trainFinishTransform.position.z, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateTrain();
        }
    }

}
