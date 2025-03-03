using UnityEngine;

public class Observer : MonoBehaviour
{
    // Detects manually if obj is being seen by the main camera

    [SerializeField] GameObject obj;
    [SerializeField] Collider objCollider;

    [SerializeField] Transform head_transform;
    public static bool observed = false;
    public static bool seeing;
    private float counter = 7f;
    [SerializeField] private float ray_length;
    Camera cam;
    Plane[] planes;

    [SerializeField] LayerMask aimColliderLayerMask;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            Ray head_ray = new Ray(transform.position, head_transform.position- transform.position);
            Debug.DrawRay(head_ray.origin, head_ray.direction.normalized * 100f, Color.blue);
            if (Physics.Raycast(head_ray, out RaycastHit hit, ray_length, aimColliderLayerMask))
            {
                if (hit.collider.gameObject.name == "PlayerArmature (2)")
                {
                   // Debug.Log(head_ray);
                    observed = true;
                    counter = 6.5f;
                    seeing = true;
                }
                else
                {
                    seeing = false;
                    counter -= Time.deltaTime;
                    if (counter <= 0) observed = false;
                }
            }
            else
            {
                seeing = false;
                counter -= Time.deltaTime;
                if (counter <= 0) observed = false;
                // Debug.Log("Nothing has been detected");
            }
            // Debug.Log(obj.name + " has been detected!");
        }
        else
        {
            seeing = false;
            counter -= Time.deltaTime;
            if (counter <= 0) observed = false;
            // Debug.Log("Nothing has been detected");
        }


    }
}