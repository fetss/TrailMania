using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    public bool LeftTouch { get; private set; }
    public bool RightTouch { get; private set; }
    void Start()
    {

    }

    void Update()
    {
        LeftTouch = false;
        RightTouch = false;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.touches[i].position.x > Screen.width / 2)
                {
                    RightTouch = true;
                }
                else if (Input.touches[i].position.x <= Screen.width / 2)
                {
                    LeftTouch = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            LeftTouch = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            RightTouch = true;
        }
    }
}
