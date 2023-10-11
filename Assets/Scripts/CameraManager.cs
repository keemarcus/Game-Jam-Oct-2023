using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetTransform;
    private PlayerManager playerManager;
    public Vector2 cameraOffset;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        targetTransform = playerManager.transform;
    }

    void Update()
    {
        if (targetTransform != null)
        {
            transform.position = new Vector3(targetTransform.position.x + (cameraOffset.x * playerManager.GetComponent<Rigidbody2D>().gravityScale), targetTransform.position.y + (cameraOffset.y * playerManager.GetComponent<Rigidbody2D>().gravityScale), -10f);
        }
    }
}
