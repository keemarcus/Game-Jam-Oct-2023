using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform targetTransform;

    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerLocomotion>().transform;
    }

    void Update()
    {
        if (targetTransform != null)
        {
            transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, -10f);
        }
    }
}
