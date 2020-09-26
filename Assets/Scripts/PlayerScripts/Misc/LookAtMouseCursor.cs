using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouseCursor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayLength;

        if(groundPlane.Raycast(rayFromCamera, out rayLength))
        {
            Vector3 lookPosition = rayFromCamera.GetPoint(rayLength);

            transform.LookAt(new Vector3(lookPosition.x, transform.position.y, lookPosition.z));
        }
    }
}
