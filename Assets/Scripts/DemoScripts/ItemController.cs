using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public LayerMask hitLayer;
    public LayerMask tableLayer;
    private RaycastHit hit;
    private RaycastHit tableHit;
    public GameObject player;
    public GameObject handItem;
    public GameObject tableItem;
    private Rigidbody rb;
    public float speed = 5.0f;
    public bool isHand = false;
    public FixedJoystick joystick;
    [SerializeField] private Camera cam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    void Update()
    {
        PlayerMovement();

        /*if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(player.transform.position,transform.forward,out hit, 3, hitLayer))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                   //print(hit.collider.name);
                   //Destroy(hit.collider.gameObject);
                   hit.collider.gameObject.SetActive(false);
                   handItem.gameObject.SetActive(true);
                   isHand = true;   
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (isHand == true)
            {
                handItem.gameObject.SetActive(false);
                hit.collider.gameObject.SetActive(true);
            }
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer))
            {
                float distance = Vector3.Distance(player.transform.position, hit.point);
                if(distance <= 3f)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            hit.collider.gameObject.SetActive(false);
                            handItem.gameObject.SetActive(true);
                            isHand = true;
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray2, out tableHit, Mathf.Infinity, tableLayer))
            {
                float distance2 = Vector3.Distance(player.transform.position, tableHit.point);
                if (distance2 <= 3f)
                {
                    if (tableHit.collider != null)
                    {
                        if (tableHit.collider.CompareTag("Table"))
                        {
                            if (isHand == true)
                            {
                                handItem.gameObject.SetActive(false);
                                tableItem.gameObject.SetActive(true);
                                isHand = false;
                            }
                           
                        }
                    }
                }
            }
        }

    }
    void PlayerMovement()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        rb.linearVelocity = direction * speed;
    }

}
