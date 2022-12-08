using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling objectPoolingInstance;

    [SerializeField] GameObject stairPrefab;
    [SerializeField] int amtToInstantiate;
    [SerializeField] Transform stairsContainer;
 
    Queue<GameObject> pooledStairs = new Queue<GameObject>();

    private void Awake()
    {
        if (objectPoolingInstance == null)
            objectPoolingInstance = this;
        else
            Destroy(this);

        InstantiateSteps();
    }
    void InstantiateSteps()
    {
        for (int i = 0; i < amtToInstantiate; i++)
        {
            GameObject stair = Instantiate(stairPrefab);
            stair.transform.SetParent(stairsContainer);
            stair.SetActive(false);
            pooledStairs.Enqueue(stair);
        }
    }

    public GameObject TakeStairFromPool()
    {
        return pooledStairs.Dequeue();
    }

    public void ReturnStairToPool(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        pooledStairs.Enqueue(obj);
    }
}
