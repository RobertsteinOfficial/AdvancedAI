using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float followSpeed = 3;
    public Vector3 ousideFollowOffset;
    public Vector3 insideFollowOffset;
    [Header("Looking")]
    public float lookSpeed = 3;
    public Vector3 outsideLookOffset;
    public Vector3 insideLookOffset;

    //local
    static CameraController instance;
    bool playerInside = false;
    Vector3 lookPosition;
    Vector3 newPosition;
    Vector3 actualFollowOffset;
    Vector3 actualLookOffset;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    private void OnValidate()
    {
        if (playerInside)
        {
            actualFollowOffset = insideFollowOffset;
            actualLookOffset = insideLookOffset;
        }
        else
        {
            actualFollowOffset = ousideFollowOffset;
            actualLookOffset = outsideLookOffset;
        }
    }

    private void Start()
    {
        actualFollowOffset = ousideFollowOffset;
        actualLookOffset = outsideLookOffset;
    }

    void LateUpdate()
    {
        //calculate follow pos
        newPosition = PlayerController.PlayerTransform.position;
        newPosition += PlayerController.PlayerTransform.right * actualFollowOffset.x;
        newPosition += Vector3.up * actualFollowOffset.y;
        newPosition -= PlayerController.PlayerTransform.forward * actualFollowOffset.z;
        //update position
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        //calculate look pos
        lookPosition = PlayerController.PlayerTransform.position;
        lookPosition += PlayerController.PlayerTransform.right * actualLookOffset.x;
        lookPosition += Vector3.up * actualLookOffset.y;
        lookPosition -= PlayerController.PlayerTransform.forward * actualLookOffset.z;
        Quaternion newRotation = Quaternion.LookRotation(lookPosition - transform.position, Vector3.up);
        //update looking
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, lookSpeed * Time.deltaTime);
    }

    public static void PlayerInside(bool value)
    {
        instance.playerInside = value;

        if (value)
        {
            instance.actualFollowOffset = instance.insideFollowOffset;
            instance.actualLookOffset = instance.insideLookOffset;
        }
        else
        {
            instance.actualFollowOffset = instance.ousideFollowOffset;
            instance.actualLookOffset = instance.outsideLookOffset;
        }
    }
}
