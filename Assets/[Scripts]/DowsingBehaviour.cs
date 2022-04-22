using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowsingBehaviour : MonoBehaviour
{
    public GameObject pickup;
    public GameObject GameController;
    public GameObject capsule;

    // Update is called once per frame
    void Update()
    {
        if (GameController.GetComponent<GameController>().timeRemaining > 5)
        {
            capsule.SetActive(false);
        }
        else
        {
            capsule.SetActive(true);
            capsule.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f,
                (5.0f - GameController.GetComponent<GameController>().timeRemaining) / 10);
            transform.LookAt(pickup.transform);
        }
        
    }
}
