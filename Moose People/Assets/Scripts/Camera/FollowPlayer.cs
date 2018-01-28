using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        //calc offset
        offset = transform.position - target.position;

    }

    void FixedUpdate()
    {
        // create posotion the camera is aiming for based on offset from target
        Vector3 targetCamPos = target.position + offset;

        //smooth interpolate camera current pos and target pos
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

    }
}
