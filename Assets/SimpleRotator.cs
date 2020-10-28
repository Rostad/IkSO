using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{

    private const float BASE_ROTATION_SPEED = 1f;

    public float rotationSpeed = BASE_ROTATION_SPEED;

    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.World);
    }
}
