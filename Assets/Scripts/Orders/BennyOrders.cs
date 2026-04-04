using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Drawing.Text;

public class Orders
{
    //description for the order
    public List<int> iceCreams = new List<int>();
    public string name;
    public bool complete;
    public bool timeFailed;
    public bool IsActive = false;
    public GameObject ticket;

    //how many of the ice creams are correct
    public int time;
    public float timer = 0f;
}

public class BennyOrders : MonoBehaviour
{
    private List<string> names = new List<string>()
    {
        "Alex","Sam","Jamie","Taylor","Casey","Morgan","Riley","Jordan","Parker","Quinn",
        "Avery","Cameron","Blake","Charlie","Elliot","Kai","Leo","Noah","Liam","Logan",
        "Lucas","Ethan","Mason","Elijah","James","Benjamin","Henry","Jack","Daniel",
        "Matthew","Samuel","David","Joseph","Owen","Wyatt","Luke","Nathan","Connor",
        "Tyler","Hunter","Evan","Cole","Tristan","Victor","Oscar","Adrian","Marco",
        "Diego","Mateo","Carlos","Javier","Ricardo","Santiago",

        "Sarah","Emily","Grace","Lily","Anna","Claire","Sophie","Hannah","Julia","Leah",
        "Madeline","Elena","Bianca","Valeria","Camila","Daniela","Lucia","Marina",
        "Olivia","Emma","Ava","Sophia","Isabella","Mia","Charlotte","Amelia","Harper",
        "Evelyn","Abigail","Ella","Scarlett","Aria","Nova","Luna","Hazel","Willow",

        "Fabio","Rania","Rowan","Natalia","Naia","Rebecca","Skye","Silver","Mungus",
        "Vinit","Khoi","Hugh","Ronald","Craig","Aidan","Andy","Michelle","Jason",

        "Batman","Spider-Man","Venom","Superman","Joker","Harley Quinn",
        "Goku","Naruto","Levi","Mikasa","Eren",

        "Sonic","Shadow","Tails","Knuckles",
        "Mario","Luigi","Peach",
        "Link","Zelda",
        "Kratos","Atreus",
        "Kirby"
    };

    //lists flavours for each of Benny's arms
    public List<int> leftOrder, rightOrder = new List<int>();
    public List<Orders> levelOrders = new List<Orders>();
    public IceCreamSpawner spawner;

    public int numOrders = 5;

    public GameObject ticket, floor;
    public Transform UIcontainer;

    public float score;
    public int stars = 0;

    public TextMeshProUGUI scoreText;

    public float finishedTimer = 2;

    Orders createOrder(int size, int time, bool active)
    {
        Orders order = new Orders();
        int nameIndex = Random.Range(0, names.Count);
        order.name = names[nameIndex];
        order.time = time;

        for (int i = 0; i < size; i++)
        {
            int Rand = Random.Range(0, 3);
            order.iceCreams.Add(Rand);
        }
        levelOrders.Add(order);

        order.ticket = Instantiate(ticket);
        GameObject ticketUI = order.ticket;
        order.IsActive = active;

        ticketUI.GetComponent<OrderUI>().BennyOrders = this;

        ticketUI.SetActive(active);

        ticketUI.transform.SetParent(UIcontainer);
        ticketUI.name = "Ticket" + (levelOrders.Count);

        order.ticket.GetComponent<OrderUI>().SetupOrderVisuals(order, levelOrders.IndexOf(order));
        return order;
    }

    float scoreCalc(Orders order)
    {
        float score = (order.timer / order.time) + 0.5f;
        if (score > 1) { score = 1; }

        return score;
    }

    void checkComplete(Orders order)
    {
        OrderUI UI = order.ticket.GetComponent<OrderUI>();

        //resets how many ice creams are correct every frame
        int leftA = 0;
        int rightA = 0;

        //checks if the flavour is correct on either side for however many scoops there are in the order
        for (int i = 0; i < order.iceCreams.Count; i++)
        {

            //if the order isn't already complete then do it
            if (!order.complete)
            {

                //if the ice creams on either hand is equal to the same position on order, then add to "leftA" (however many are correct)
                //if the amount of scoops in the left and right order is less than the value of i, it won't check it
                if (i < leftOrder.Count && leftOrder[i] == order.iceCreams[i])
                {
                    leftA++;
                }

                if (i < rightOrder.Count && rightOrder[i] == order.iceCreams[i])
                {
                    rightA++;
                }
            }

        }

        // print("The correct amount on the right is " + rightA);
        // print("The correct amount on the left is " + leftA);

        //if leftA or rightA (however many ice creams are correct) is equal to the amount of ice creams in the order, mark the order as complete
        if (leftA == order.iceCreams.Count && !order.timeFailed)
        {

            UI.MarkAsComplete();
            order.complete = true;

            score += scoreCalc(order);

            Debug.Log(scoreCalc(order));
            scoreText.text = (score * 100).ToString("F0");

            for (int j = 0; j < order.iceCreams.Count; j++)
            {
                Destroy(transform.GetChild(1).GetChild(leftOrder.Count - 1).gameObject);
                leftOrder.RemoveAt(leftOrder.Count - 1);
            }

        }
        else if (rightA == order.iceCreams.Count && !order.timeFailed)
        {
            UI.MarkAsComplete();
            order.complete = true;

            score += scoreCalc(order);
            scoreText.text = (score * 100).ToString("F0");

            for (int j = 0; j < order.iceCreams.Count; j++)
            {
                Destroy(transform.GetChild(0).GetChild(rightOrder.Count - 1).gameObject);
                rightOrder.RemoveAt(rightOrder.Count - 1);
            }
        }
    }

    private IEnumerator countDown(Orders order)
    {
        TextMeshProUGUI timerUI = order.ticket.GetComponent<OrderUI>().timerText;

        order.timer = order.time;

        while (order.timer >= 0 && !order.complete && order.IsActive)
        {
            yield return new WaitForSeconds(1);
            order.timer -= 1;
            timerUI.text = "Time:" + order.timer;
        }

        if (!order.complete)
        {
            order.timeFailed = true;
            timerUI.text = "Failed";
        }

        int orderIndex = levelOrders.IndexOf(order);

        yield return new WaitForSeconds(2);
        Destroy(order.ticket);
        levelOrders.Remove(order);

        foreach (Orders orders in levelOrders)
        {
            int index = levelOrders.IndexOf(orders);
            if (orders.IsActive) { orders.ticket.GetComponent<OrderUI>().setPosition(orders, index); }
        }

        foreach (Orders nextInLine in levelOrders)
        {
            if (!nextInLine.IsActive)
            {
                nextInLine.IsActive = true;
                int newIndex = levelOrders.IndexOf(nextInLine);

                nextInLine.ticket.GetComponent<OrderUI>().SetupOrderVisuals(nextInLine, newIndex);
                StartCoroutine(countDown(nextInLine));

                break;
            }
        }
    }
    //randomly generates a set of ice creams to complete
    void Start()
    {
        floor = GameObject.Find("Floor");

        for (int i = 0; i < numOrders; i++)
        {   // timer logic
            bool active = false;
            int scoopCount = Random.Range(3, 6); // an order randomly has 3-5 scoops
            float timeMultiplier = DifficultyManager.timeMultiplier;
            float time = 20 + scoopCount * timeMultiplier;

            if (i == 0 || i == 1) active = true;

            Orders order = createOrder(scoopCount, Mathf.RoundToInt(time), active);

            if (order.IsActive) { StartCoroutine(countDown(order)); }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Orders order in levelOrders)
        {
            if (!order.complete && order != null)
            {
                checkComplete(order);
            }

        }
        // garbage bin at the top right corner (4, 0, 4) to destroy ice cream
        if (gameObject.transform.position.x == 4 && gameObject.transform.position.z == 4)
        {
            float rotation = gameObject.GetComponent<BennyScript>().benny_rotation;

            if (rotation == 90)
            {
                print("In the area");
                foreach (Transform iceCream in transform.Find("ArmL"))
                {
                    Destroy(iceCream.gameObject);
                }

                leftOrder.Clear();


            }
            else if (rotation == 270)
            {
                print("In the area");
                foreach (Transform iceCream in transform.Find("ArmR"))
                {
                    Destroy(iceCream.gameObject);
                }

                rightOrder.Clear();

            }
        }

        if (levelOrders.Count == 0)
        {
            stars = (int)((score / numOrders) * 5) + 1;
            if (stars > 5) stars = 5;

            print("The star count was" + stars);

            finishedTimer -= Time.deltaTime;
            if (finishedTimer < 0)
            {
                if (gameObject.name == "Benny") // lvl 1
                {
                    PlayerPrefs.SetInt("L1Stars", stars);
                    if (stars > 2)
                    {
                        SceneManager.LoadScene("WinMenu1");
                    }
                    else
                    {
                        SceneManager.LoadScene("LoseMenu1");
                    }
                }
                else if (gameObject.name == "BennyCowboy") // lvl 2
                {
                    PlayerPrefs.SetInt("L2Stars", stars);
                    if (stars > 2)
                    {
                        SceneManager.LoadScene("WinMenu2");
                    }
                    else
                    {
                        SceneManager.LoadScene("LoseMenu2");
                    }
                }
                else if (gameObject.name == "BennyAstro") // lvl 3
                {
                    PlayerPrefs.SetInt("L3Stars", stars);
                    if (stars > 2)
                    {
                        SceneManager.LoadScene("WinMenu3");
                    }
                    else
                    {
                        SceneManager.LoadScene("LoseMenu3");
                    }
                }

            }
        }
    }
}
