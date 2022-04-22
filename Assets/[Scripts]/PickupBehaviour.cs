using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    private float rotationNum = 1.0f;
    void Update()
    {
        rotationNum += 0.25f;
        transform.rotation = Quaternion.Euler(0, rotationNum, 0);
    }
}
