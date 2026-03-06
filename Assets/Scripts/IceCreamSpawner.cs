using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IceCreamSpawner : MonoBehaviour
{
    public GameObject scoopOne;
    public GameObject scoopTwo;
    public GameObject scoopThree;
    public BennyOrders bennyOrders;

    public TextMeshProUGUI rightOrderText;
    public TextMeshProUGUI leftOrderText;


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

        if (timer >= 2f)
        {
            if (chance <= 33)
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 10, randomSpawnZ);
                Instantiate(scoopOne, randomPosition, Quaternion.identity);
                scoopOne.GetComponent<IceCreamScript>().bennyOrders = bennyOrders;
                scoopOne.GetComponent<IceCreamScript>().leftOrderText = leftOrderText;
                scoopOne.GetComponent<IceCreamScript>().rightOrderText = rightOrderText;
                timer = 0f;
            }
            else if (chance <= 66)
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 10, randomSpawnZ);
                Instantiate(scoopTwo, randomPosition, Quaternion.identity);
                scoopTwo.GetComponent<IceCreamScript>().bennyOrders = bennyOrders;
                scoopTwo.GetComponent<IceCreamScript>().leftOrderText = leftOrderText;
                scoopTwo.GetComponent<IceCreamScript>().rightOrderText = rightOrderText;
                timer = 0f;
            }
            else
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 10, randomSpawnZ);
                Instantiate(scoopThree, randomPosition, Quaternion.identity);
                scoopThree.GetComponent<IceCreamScript>().bennyOrders = bennyOrders;
                scoopThree.GetComponent<IceCreamScript>().leftOrderText = leftOrderText;
                scoopThree.GetComponent<IceCreamScript>().rightOrderText = rightOrderText;
                timer = 0f;
            }
        }
    }
}
