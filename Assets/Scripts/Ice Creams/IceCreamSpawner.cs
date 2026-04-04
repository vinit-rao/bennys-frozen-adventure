using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class IceCreamSpawner : MonoBehaviour
{
    public GameObject scoopOne, scoopTwo, scoopThree;
    public BennyOrders bennyOrders;

    public float chunkTime;

    private float chunkTimer = 0f;
    public List<int> currentChunk = new List<int> { 0, 1, 2 };

    public GameObject spotLight;

    public TextMeshProUGUI rightOrderText;
    public TextMeshProUGUI leftOrderText;

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
        StartCoroutine(IceCreams(currentChunk));

        chunkTime = DifficultyManager.spawnRate * 3;

    }

    //fisher yates algorithm
    public static void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void Update()
    {
        // Debug.Log("spawn rate: " + DifficultyManager.spawnRate);
        // Debug.Log("chunk time: " + chunkTime);
        // Debug.Log("fall speed: " + DifficultyManager.fallSpeed);
        chunkTimer += Time.deltaTime;

        if (chunkTimer >= chunkTime)
        {
            StartCoroutine(IceCreams(new List<int>(currentChunk)));

            chunkTimer = 0f;
        }
    }

    IEnumerator IceCreams(List<int> chunk)
    {
        Shuffle(chunk);
        foreach (int flavour in chunk)
        {
            Vector3 randomPosition =
            new Vector3(Mathf.RoundToInt(Random.Range(-minSpawn, minSpawn)),
            18, Mathf.RoundToInt(Random.Range(-minSpawn, minSpawn)));

            GameObject prefab = null;
            Color color = Color.white;

            if (flavour == 0)
            {
                prefab = scoopOne;

                if (prefab.name == "ScoopStrawberry")
                {
                    color = new Color(0.9f, 0.3f, 0.5f); ;
                }

                else if (prefab.name == "ScoopRockyRoad")
                {
                    color = new Color(1.0f, 0.3f, 0.3f);
                }

                else
                {
                    color = new Color(0.8f, 0.5f, 1.0f);
                }
            }
            if (flavour == 1)
            {
                prefab = scoopTwo;

                if (prefab.name == "ScoopChoc")
                {
                    color = new Color(1.0f, 0.3f, 0.0f);
                }

                else if (prefab.name == "ScoopPistachio")
                {
                    color = new Color(0.2f, 1.0f, 0.0f);
                }

                else
                {
                    color = new Color(0.2f, 0.8f, 1.0f);
                }
            }
            if (flavour == 2)
            {
                prefab = scoopThree;

                if (prefab.name == "ScoopVanilla")
                {
                    color = Color.white;
                }

                else if (prefab.name == "ScoopButterscotch")
                {
                    color = new Color(1.0f, 0.8f, 0.3f);
                }
                else
                {
                    color = new Color(0.2f, 0.6f, 0.2f);
                }
            }

            GameObject scoop = Instantiate(prefab, randomPosition, Quaternion.identity);
            GameObject light = Instantiate(spotLight, randomPosition, Quaternion.identity);
     
            light.transform.Rotate(90, 0, 0);
            light.transform.SetParent(scoop.transform);
            light.transform.GetComponent<Light>().color = color;

            yield return new WaitForSeconds(DifficultyManager.spawnRate);
        }
    }
}
