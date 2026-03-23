using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IceCreamSpawner : MonoBehaviour
{
    public GameObject scoopOne, scoopTwo, scoopThree;
    public BennyOrders bennyOrders;
    public AudioSource music;

    public float timeBetween = 2;

    public GameObject spotLight;

    public TextMeshProUGUI rightOrderText;
    public TextMeshProUGUI leftOrderText;


    float timer = 0f;

    public float minSpawn = 4;
    private void Start()
    {
        GameObject scoop = scoopOne;

        for (int i = 0; i < 3; i++)
        {
            if (i == 1) scoop = scoopTwo;
            if (i == 2) scoop = scoopThree;

            scoop.GetComponent<IceCreamScript>().bennyOrders = bennyOrders;
            scoop.GetComponent<IceCreamScript>().leftOrderText = leftOrderText;
            scoop.GetComponent<IceCreamScript>().rightOrderText = rightOrderText;
            scoop.GetComponent<IceCreamScript>().spawner = this;
        }
    }

    void Update()
    {
        int chance = Random.Range(0, 3);
        Vector3 randomPosition =
        new Vector3(Mathf.RoundToInt(Random.Range(-minSpawn, minSpawn)),
        20, Mathf.RoundToInt(Random.Range(-minSpawn, minSpawn)));

        timer += Time.deltaTime;

        if (timer >= timeBetween)
        {
            GameObject light = Instantiate(spotLight, randomPosition, Quaternion.identity);
            GameObject scoop;
            light.transform.Rotate(90, 0, 0);

            if (chance == 0)
            {
                scoop = Instantiate(scoopOne, randomPosition, Quaternion.identity);
                
                light.transform.SetParent(scoop.transform);
                light.transform.GetComponent<Light>().color = new Color(0.9f, 0.3f, 0.5f);
            }
            else if (chance == 1)
            {
                scoop = Instantiate(scoopTwo, randomPosition, Quaternion.identity);

                light.transform.SetParent(scoop.transform);
                light.transform.GetComponent<Light>().color = new Color(1.0f, 0.5f, 0.0f);
            }
            else
            {
                scoop = Instantiate(scoopThree, randomPosition, Quaternion.identity);

                light.transform.SetParent(scoop.transform);
                light.transform.GetComponent<Light>().color = Color.white;
            }

            timer = 0f;
            
        }
    }
}
