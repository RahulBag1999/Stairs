using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject follower;

    private Vector3 offest;
    
    void Start()
    {
        offest = transform.position - follower.transform.position;
    }

    
    private void FixedUpdate()
    {
        transform.position = follower.transform.position + offest;
    }
}
