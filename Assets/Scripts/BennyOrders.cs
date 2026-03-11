using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class orders
{
    //description for the order
    public List<int> iceCreams = new List<int>();
    public string name;
    public bool complete;

    //how many of the ice creams are correct
    public int leftA = 0;
    public int rightA = 0;
}

public class BennyOrders : MonoBehaviour
{
    //lists flavors for each of Benny's arms
    public List<int> leftOrder = new List<int>();
    public List<int> rightOrder = new List<int>();

    //how many ice creams benny is holding in each hand
    private int leftCount = 0;
    private int rightCount = 0;

    orders Order1;
    orders Order2;
    orders Order3;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    orders createOrder(TextMeshProUGUI text, int size)
    {
        text.text = "";
        orders order = new orders();
        for (int i = 0; i < size; i++)
        {
            int Rand = Random.Range(0, 3);
            order.iceCreams.Add(Rand);

            switch (Rand)
            {
                case 0:
                    text.text += "Strawberry, ";
                    break;
                case 1:
                    text.text += "Vanilla, ";
                    break;
                case 2:
                    text.text += "Chocolate, ";
                    break;
            }
        }

        return order;
    }

    //randomly generates a set of ice creams to complete
    void Start()
    {
        Order1 = createOrder(text1, 3);
        Order2 = createOrder(text2, 3);
        Order3 = createOrder(text3, 3);
    }

    void checkComplete(orders order, TextMeshProUGUI text)
    {
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

            //if leftA or rightA (however many ice creams are correct) is equal to the amount of ice creams in the order, mark the order as complete
            if (leftA == order.iceCreams.Count)
            {
                text.text = "Complete";
                order.complete = true;

                for (int j = 0; j < 3; j++)
                {
                    Destroy(transform.GetChild(1).GetChild(leftOrder.Count - 1).gameObject);
                    leftOrder.RemoveAt(leftOrder.Count - 1);
                    leftCount -= 1;

                    leftText.text = "Left :";
                }

            }
            else if (rightA == order.iceCreams.Count)
            {
                text.text = "Complete";
                order.complete = true;

                for (int j = 0; j < 3; j++)
                {
                    Destroy(transform.GetChild(0).GetChild(rightOrder.Count - 1).gameObject);
                    rightOrder.RemoveAt(rightOrder.Count - 1);
                    rightCount -= 1;
                    rightText.text = "Right :";
                }

            }


            //update left and right count
            leftCount = leftOrder.Count;
            rightCount = rightOrder.Count;

        }
    }

    // Update is called once per frame
    void Update()
    {

        //checks if any ice creams have been added to the left or right hand
        if (leftCount < leftOrder.Count || rightCount < rightOrder.Count)
        {
            checkComplete(Order1, text1);
            checkComplete(Order2, text2);
            checkComplete(Order3, text3);
        }

        if (gameObject.transform.position.x == 4 && gameObject.transform.position.z == 4)
        {
            
            if (gameObject.GetComponent<BennyScript>().benny_rotation == 90)
            {
                for (int i = 0; i < leftOrder.Count; i++)
                {
                    Destroy(transform.GetChild(1).GetChild(i).gameObject);
                    leftOrder.RemoveAt(0);
                    leftCount = 0;

                    leftText.text = "Left: ";
                }
            } else if (gameObject.GetComponent<BennyScript>().benny_rotation == 270)
            {
                for (int i = 0; i < rightOrder.Count; i++)
                {
                    Destroy(transform.GetChild(0).GetChild(i).gameObject);
                    rightOrder.RemoveAt(0);
                    rightCount = 0;

                    rightText.text = "Right: ";
                }
            }
        }
    }
}
