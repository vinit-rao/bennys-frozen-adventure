using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamSpawner : MonoBehaviour
{
    public GameObject scoopOne;
    public GameObject scoopTwo;
    public GameObject scoopThree;

    float timer = 0f;

    public float minSpawnX = 4f;
    public float maxSpawnX = -4f;
    public float minSpawnZ = 4f;
    public float maxSpawnZ = -4f;

    void Update()
    {
        int chance = Random.Range(1, 101);
        int randomSpawnX = Mathf.RoundToInt(Random.Range(minSpawnX, maxSpawnX));
        int randomSpawnZ = Mathf.RoundToInt(Random.Range(minSpawnZ, maxSpawnZ));

        timer += Time.deltaTime;

        if (timer >= 3f)
        {
            if (chance <= 33)
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 10, randomSpawnZ);
                Instantiate(scoopOne, randomPosition, Quaternion.identity);
                timer = 0f;
            }
            else if (chance <= 66)
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 10, randomSpawnZ);
                Instantiate(scoopTwo, randomPosition, Quaternion.identity);
                timer = 0f;
            }
            else
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 10, randomSpawnZ);
                Instantiate(scoopThree, randomPosition, Quaternion.identity);
                timer = 0f;
            }
        }
    }
}
