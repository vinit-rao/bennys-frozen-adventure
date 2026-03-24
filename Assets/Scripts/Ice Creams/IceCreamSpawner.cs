using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IceCreamSpawner : MonoBehaviour
{
    public GameObject scoopOne, scoopTwo, scoopThree;
    public BennyOrders bennyOrders;

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
                // scoopOne (inspector assigned) lvl 1 always strawberry, lvl 2 always rocky road, lvl 3 always lavender

                if (scoop.name == "ScoopStrawberry(Clone)")
                {
                    light.transform.GetComponent<Light>().color = new Color(0.9f, 0.3f, 0.5f); ;
                }

                else if (scoop.name == "ScoopRockyRoad(Clone)")
                {
                    light.transform.GetComponent<Light>().color = new Color(1.0f, 0.3f, 0.3f);
                }

                else
                {
                    // for space lvl 3;
                }
            }

            else if (chance == 1)
            {
                scoop = Instantiate(scoopTwo, randomPosition, Quaternion.identity);
                // scoopTwo (inspector assigned) lvl 1 always choc, lvl 2 always pistachio, lvl 3 always blue moon
                light.transform.SetParent(scoop.transform);

                if (scoop.name == "ScoopChocolate(Clone)")
                {
                    light.transform.GetComponent<Light>().color = new Color(1.0f, 0.3f, 0.0f);
                }
                
                else if (scoop.name == "ScoopPistachio(Clone)")
                {
                    light.transform.GetComponent<Light>().color = new Color(0.2f, 1.0f, 0.0f);
                }
                
                else
                {
                    // for space lvl 3;
                }
            }

            else
            {
                scoop = Instantiate(scoopThree, randomPosition, Quaternion.identity);
                // scoopThree (inspector assigned) lvl 1 always vanilla, lvl 2 always butterscotch, lvl 3 always black hole
                light.transform.SetParent(scoop.transform);
                
                if (scoop.name == "ScoopVanilla(Clone)")
                {
                    light.transform.GetComponent<Light>().color = Color.white;
                }
                
                else if (scoop.name == "ScoopButterscotch(Clone)")
                {
                    light.transform.GetComponent<Light>().color = new Color(1.0f, 0.8f, 0.3f);

                }

                else
                {
                    // for space lvl 3;
                }
            }

            timer = 0f;
            
        }
    }
}
