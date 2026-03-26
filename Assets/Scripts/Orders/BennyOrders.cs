using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public int leftA = 0;
    public int rightA = 0;
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

    //lists flavors for each of Benny's arms
    public List<int> leftOrder = new List<int>();
    public List<int> rightOrder = new List<int>();
    public List<Orders> levelOrders = new List<Orders>();
    public IceCreamSpawner spawner;

    public int numOrders = 5;
    Coroutine startTimer;

    //how many ice creams benny is holding in each hand
    private int leftCount = 0;
    private int rightCount = 0;
    public GameObject ticket;
    public Transform UIcontainer;

    public int score;

    public GameObject floor;

    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    Orders createOrder(int size, float time, bool active)
    {
        Orders order = new Orders();
        int nameIndex = Random.Range(0, names.Count);
        order.name = names[nameIndex];
        order.timer = time;

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

        ticketUI.transform.SetParent(UIcontainer);
        ticketUI.name = "Ticket" + (levelOrders.Count);

        order.ticket.GetComponent<OrderUI>().SetupOrderVisuals(order, levelOrders.IndexOf(order));
        return order;
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

    //randomly generates a set of ice creams to complete
    void Start()
    {
        floor = GameObject.Find("Floor");

        for (int i = 0; i < numOrders; i++)
        {
            bool active = false;
            int random = Random.Range(3, 8);
            int time = (random * 6) + 15;

            if (time > 25 && time < 35)
            {
                time = 30;
            } else if (time > 35 && time < 45)
            {
                time = 40;
            } else if (time > 45 && time < 60)
            {
                time = 45;
            } else if (time > 60 && time < 75)
            {
                time = 70;
            }

            if (i == 0 || i == 1) active = true;

            Orders order = createOrder(random, time, active);

            if (order.IsActive) { startTimer = StartCoroutine(countDown(order)); }
        }
    }

    void checkComplete(Orders order)
    {
        OrderUI UI = order.ticket.GetComponent<OrderUI>();

        //resets how many ice creams are correct every frame
        int leftA = 0;
        int rightA = 0;

        //checks if the flavor is correct on either side for however many scoops there are in the order
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

            //update left and right count
            leftCount = leftOrder.Count;
            rightCount = rightOrder.Count;
        }

        //if leftA or rightA (however many ice creams are correct) is equal to the amount of ice creams in the order, mark the order as complete
        if (leftA == order.iceCreams.Count)
        {
            UI.MarkAsComplete();
            order.complete = true;

            for (int j = 0; j < order.iceCreams.Count; j++)
            {
                Destroy(transform.GetChild(1).GetChild(leftOrder.Count - 1).gameObject);
                leftOrder.RemoveAt(leftOrder.Count - 1);
                leftCount -= 1;

                leftText.text = "Left :";
            }

        }
        else if (rightA == order.iceCreams.Count)
        {
            UI.MarkAsComplete();
            order.complete = true;

            for (int j = 0; j < order.iceCreams.Count; j++)
            {
                Destroy(transform.GetChild(0).GetChild(rightOrder.Count - 1).gameObject);
                rightOrder.RemoveAt(rightOrder.Count - 1);
                rightCount -= 1;
                rightText.text = "Right :";
            }
        }
    }

    private IEnumerator countDown(Orders order)
    {
        TextMeshProUGUI timerUI = order.ticket.GetComponent<OrderUI>().timerText;

        while (order.timer >= 0)
        {
            yield return new WaitForSeconds(1);
            order.timer -= 1;
            timerUI.text = "Time" + order.timer;
        }

        if (order.complete)
        {
            yield return new WaitForSeconds(2);
            Destroy(order.ticket);
            levelOrders.Remove(order);
        } else
        {
            order.timeFailed = true;
            timerUI.text = "Failed";
            

            yield return new WaitForSeconds(2);
            Destroy(order.ticket);
            levelOrders.Remove(order);
        }

        int orderIndex = levelOrders.IndexOf(order);

        foreach (Orders orders in levelOrders)
        {
            int index = levelOrders.IndexOf(orders);
            orders.ticket.GetComponent<OrderUI>().setPosition(orders, index);
            print(index);
        }

        int nextIndex = orderIndex + 1;
        Orders nextOrder = levelOrders[nextIndex];

        if (orderIndex == 1)
        {
            nextIndex = orderIndex;
            nextOrder = levelOrders[nextIndex];
        }

        nextOrder.IsActive = true;
        nextOrder.ticket.GetComponent<OrderUI>().SetupOrderVisuals(nextOrder, nextIndex);
        if (nextOrder.IsActive) { startTimer = StartCoroutine(countDown(nextOrder)); }
    }

    // Update is called once per frame
    void Update()
    {
        //checks if any ice creams have been added to the left or right hand
        foreach (Orders order in levelOrders)
        {
            if (order.timer != 0)
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

                leftText.text = "Left: ";

                leftCount = leftOrder.Count;

            } else if (rotation == 270)
            {
                print("In the area");
                foreach (Transform iceCream in transform.Find("ArmR"))
                {
                    Destroy(iceCream.gameObject);
                }

                rightOrder.Clear();

                rightText.text = "Right: ";
                rightCount = rightOrder.Count;
            }
        }
    }
}
