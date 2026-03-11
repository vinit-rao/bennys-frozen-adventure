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
    public AudioSource music;

    public float timeBetween = 2f;

    public GameObject spotLight;

    public TextMeshProUGUI rightOrderText;
    public TextMeshProUGUI leftOrderText;


    float timer = 0f;

    public float minSpawnX = 4f;
    public float maxSpawnX = -4f;
    public float minSpawnZ = 4f;
    public float maxSpawnZ = -4f;

    private void Start()
    {
        scoopOne.GetComponent<IceCreamScript>().bennyOrders = bennyOrders;
        scoopOne.GetComponent<IceCreamScript>().leftOrderText = leftOrderText;
        scoopOne.GetComponent<IceCreamScript>().rightOrderText = rightOrderText;
        scoopOne.GetComponent<IceCreamScript>().spawner = this;
        scoopOne.GetComponent<IceCreamScript>().music = music;

        scoopTwo.GetComponent<IceCreamScript>().bennyOrders = bennyOrders;
        scoopTwo.GetComponent<IceCreamScript>().leftOrderText = leftOrderText;
        scoopTwo.GetComponent<IceCreamScript>().rightOrderText = rightOrderText;
        scoopTwo.GetComponent<IceCreamScript>().spawner = this;
        scoopTwo.GetComponent<IceCreamScript>().music = music;

        scoopThree.GetComponent<IceCreamScript>().bennyOrders = bennyOrders;
        scoopThree.GetComponent<IceCreamScript>().leftOrderText = leftOrderText;
        scoopThree.GetComponent<IceCreamScript>().rightOrderText = rightOrderText;
        scoopThree.GetComponent<IceCreamScript>().spawner = this;
        scoopThree.GetComponent<IceCreamScript>().music = music;
    }

    void Update()
    {
        int chance = Random.Range(1, 101);
        int randomSpawnX = Mathf.RoundToInt(Random.Range(minSpawnX, maxSpawnX));
        int randomSpawnZ = Mathf.RoundToInt(Random.Range(minSpawnZ, maxSpawnZ));

        timer += Time.deltaTime;

        if (timer >= timeBetween)
        {
            if (chance <= 33)
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 20, randomSpawnZ);
                GameObject scoop1 = Instantiate(scoopOne, randomPosition, Quaternion.identity);
                GameObject light = Instantiate(spotLight, randomPosition, Quaternion.identity);
                light.transform.SetParent(scoop1.transform);
                light.transform.Rotate(90, 0, 0);
                light.transform.GetComponent<Light>().color = new Color(1.0f, 0.5f, 0.0f);

                scoop1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

                timer = 0f;
            }
            else if (chance <= 66)
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 20, randomSpawnZ);
                GameObject scoop2 = Instantiate(scoopTwo, randomPosition, Quaternion.identity);
                scoop2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                GameObject light = Instantiate(spotLight, randomPosition, Quaternion.identity);
                light.transform.SetParent(scoop2.transform);
                light.transform.Rotate(90, 0, 0);
                light.transform.GetComponent<Light>().color = new Color(0.9f, 0.3f, 0.5f);

                timer = 0f;
            }
            else
            {
                Vector3 randomPosition = new Vector3(randomSpawnX, 20, randomSpawnZ);
                GameObject scoop3 = Instantiate(scoopThree, randomPosition, Quaternion.identity);
                scoop3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                GameObject light = Instantiate(spotLight, randomPosition, Quaternion.identity);
                light.transform.SetParent(scoop3.transform);
                light.transform.Rotate(90, 0, 0);
                light.transform.GetComponent<Light>().color = Color.white;

                timer = 0f;
            }
        }
    }
}
