using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamSpawner : MonoBehaviour
{
    public GameObject scoopOne;
    public GameObject scoopTwo;
    public GameObject scoopThree;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));
            Instantiate(scoopOne, randomSpawnPosition, Quaternion.identity);
        }
    }
}
