using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] float startSpeed;
    [SerializeField] float force;

    float acceleration, speed;

    
    void Start()
    {
        speed = startSpeed;
        acceleration = -startSpeed * force;
    }

    
    void Update()
    {
        if (transform.localPosition.y < -1f)
            GameManager.instance.GameOver();
    }

    private void FixedUpdate()
    {
        speed += acceleration * Time.fixedDeltaTime;
        Vector3 temp = new Vector3(0, speed * Time.fixedDeltaTime, 0);
        transform.localPosition += temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = startSpeed;
        GameManager.instance.SpawnBlock();

        if (!GameManager.instance.hasGameStarted) return;
        if(collision.gameObject.CompareTag("Step"))
        {
            //Destroy(collision.gameObject, 5f);
            ObjectPooling.objectPoolingInstance.ReturnStairToPool(collision.gameObject);
            GameManager.instance.UpdateScore();
        }
        else if(collision.gameObject.CompareTag("Combo"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.UpdateCombo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Diamond"))
        {
            GameManager.instance.UpdateDiamond();
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Spike"))
        {
            GameManager.instance.GameOver();
        }
    }
}
