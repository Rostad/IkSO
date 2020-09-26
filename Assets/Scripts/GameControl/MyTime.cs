using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTime : MonoBehaviour
{

    public static MyTime instance;

    public float UnpausedTime
    {
        get; private set;
    } = 0.0f;

    public void Update()
    {
        if (!GamePauser.IsPaused)
        {
            UnpausedTime += Time.deltaTime;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
