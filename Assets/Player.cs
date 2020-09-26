using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{

    public event EventHandler OnShoot;
    public event EventHandler OnAttributeChange;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnAttributeChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
