using UnityEngine;

public class BennyIceCreamStacker : MonoBehaviour
{
    public Rigidbody rb;
    public float scoopHeight = 0.5f;
    public float stack_y;
    public Transform scoop;
    public IceCreamScript landed;
    private BennyOrders bennyOrders;

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

        // mark landed
        scoop.tag = "ScoopLanded";
    }
}