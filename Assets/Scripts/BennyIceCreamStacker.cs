using UnityEngine;

public class BennyIceCreamStacker : MonoBehaviour
{
    public Rigidbody rb;
    public float scoopHeight = 0.5f;
    public float stack_y;
    public Transform scoop;
    public IceCreamScript landed;
    public BennyOrders bennyOrders;

    void Start()
    {
        stack_y = transform.position.y;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Scoop")) return;
        IceCreamScript s = other.transform.GetComponent<IceCreamScript>();
        if (s != null && s.landed) return;
        if (s != null) s.landed = true;

        
        scoop = other.transform;

        // stop physics
        rb = scoop.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        // stack on top
        stack_y += scoopHeight;
        scoop.position = new Vector3(transform.position.x, stack_y, transform.position.z);

        // snap to tagged "Player"
        scoop.SetParent(transform);


        if (scoop.name == "ScoopStrawberry(Clone)")
        {
            if (gameObject.name == "handLeft")
            {
                print("Added straw left");
                bennyOrders.leftOrder.Add(0);
            } else
            {
                print("Added straw right");
                bennyOrders.rightOrder.Add(0);
            }
        } else if (scoop.name == "ScoopVanilla(Clone)")
        {
            if (gameObject.name == "handLeft")
            {
                print("Added vanilla left");
                bennyOrders.leftOrder.Add(1);
            }
            else
            {
                print("Added vanilla right");
                bennyOrders.rightOrder.Add(1);
            }
        } else if (scoop.name == "ScoopChoc(Clone)")
        {
            if (gameObject.name == "handLeft")
            {
                print("Added choc left");
                bennyOrders.leftOrder.Add(2);
            }
            else
            {
                print("Added choc right");
                bennyOrders.rightOrder.Add(2);
            }
        }

        // mark landed
        scoop.tag = "ScoopLanded";
    }
}