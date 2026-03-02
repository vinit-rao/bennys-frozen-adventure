using UnityEngine;

public class IceCreamScript : MonoBehaviour
{
    public float scoopHeight = 0.5f;
    public Rigidbody rb;
    public bool landed = false;
    public BennyOrders bennyOrders;

    private float timeRemaining = 5;
    GameObject collision = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void addScoop()
    {
        int leftCount = bennyOrders.leftOrder.Count;
        int rightCount = bennyOrders.rightOrder.Count;
        float stack_y = 2.25f;

        if (transform.parent.name == "handLeft")
        {
            print("Added left");

            stack_y += scoopHeight * (leftCount + 1);
            print(stack_y);

            transform.position = new Vector3(collision.transform.position.x, stack_y, collision.transform.position.z);

            switch (transform.name)
            {
                case "ScoopStrawberry(Clone)":
                    bennyOrders.leftOrder.Add(0);
                    return;

                case "ScoopVanilla(Clone)":
                    bennyOrders.leftOrder.Add(1);
                    return;

                case "ScoopChoc(Clone)":
                    bennyOrders.leftOrder.Add(2);
                    return;
            }
        }
        else
        {
            print("Added right");

            stack_y += scoopHeight * (rightCount + 1);
            print(stack_y);

            transform.position = new Vector3(collision.transform.position.x, stack_y, collision.transform.position.z);
            switch (transform.name)
            {
                case "ScoopStrawberry(Clone)":
                    bennyOrders.rightOrder.Add(0);
                    break;

                case "ScoopVanilla(Clone)":
                    bennyOrders.rightOrder.Add(1);
                    break;

                case "ScoopChoc(Clone)":
                    bennyOrders.rightOrder.Add(2);
                    break;
            }
        }
    }

    private void Update()
    {
        //deletes the ice cream after 5 seconds if the collision is the floor
        if (collision != null)
        {
            if (collision.CompareTag("Floor"))
            {
                print("Landed on ground");

                timeRemaining -= Time.deltaTime;

                if (timeRemaining <= 4)
                {
                    Destroy(transform.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //allows outside functions to see the collision
        collision = other.gameObject;

        //checks if the object collided with one of benny's arms
        if (other.transform.CompareTag("BennyArm") && !landed)
        {
            landed = true;

            //stops all movement
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            transform.position = new Vector3(other.transform.position.x, 2.5f, other.transform.position.z);

            transform.SetParent(other.transform);

            //depending on the scoop add it to the list of benny scoops
            addScoop();

        //if it lands on another ice cream scoop
        } else if (other.transform.CompareTag("Scoop") && !landed)
        {
            print("scoopstack");
            landed = true;

            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            transform.SetParent(other.transform.parent);

            addScoop();
        }
    }   
}
