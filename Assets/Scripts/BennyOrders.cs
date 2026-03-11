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
    public TextMeshProUGUI text;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    //randomly generates a set of ice creams to complete
    void Start()
    {
        Order1 = new orders();
        text.text = "";
        for (int i = 0; i < 3; i++)
        {
            int Rand = Random.Range(0, 3);
            Order1.iceCreams.Add(Rand);

            switch (Rand)
            {
                case 0:
                    text.text += "straw, ";
                    break;
                case 1:
                    text.text += "van, ";
                    break;
                case 2:
                    text.text += "choc, ";
                    break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //resets how many ice creams are correct every frame
        int leftA = 0;
        int rightA = 0;

        //checks if any ice creams have been added to the left or right hand
        if (leftCount < leftOrder.Count || rightCount < rightOrder.Count)
        {
            //checks if the flavor is correct on either side for however many scoops there are in the order
            for (int i = 0; i < Order1.iceCreams.Count; i++)
            {

                //if the order isn't already complete then do it
                if (!Order1.complete)
                {

                    //if the ice creams on either hand is equal to the same position on order1, then add to "leftA" (however many are correct)
                    //if the amount of scoops in the left and right order is less than the value of i, it won't check it
                    if (i < leftOrder.Count && leftOrder[i] == Order1.iceCreams[i])
                    {
                        leftA++;
                    }

                    if (i < rightOrder.Count && rightOrder[i] == Order1.iceCreams[i])
                    {
                        rightA++;
                    }
                }

                //if leftA or rightA (however many ice creams are correct) is equal to the amount of ice creams in the order, mark the order as complete
                if (leftA == Order1.iceCreams.Count || rightA == Order1.iceCreams.Count)
                {
                    text.text = "Complete";
                    Order1.complete = true;
                }


                //update left and right count
                leftCount = leftOrder.Count;
                rightCount = rightOrder.Count;

            }

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
