using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] Collider objCollider;
    [SerializeField] Transform head_transform;

    [SerializeField] LayerMask aimColliderLayerMask;

    [SerializeField] private float ray_length;

    private Camera cam;
    private Plane[] planes;

    private float _rememberTime = 5f;
    private float _timer;

    private bool _observed;
    private bool _observing;

    public bool observing => _observing;
    public bool observed => _observed;

    private Vector3 _playerCenterOffset = new Vector3(0, 1.3f, 0);
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            Ray head_ray = new Ray(transform.position, (head_transform.position - transform.position).normalized);
            Debug.DrawRay(head_ray.origin, head_ray.direction * 100f, Color.blue);
            if (Physics.Raycast(head_ray, out RaycastHit hit, ray_length, aimColliderLayerMask))
            {
                if (hit.collider.GetComponent<PlayerEntity>())
                {
                    _observed = true;
                    _timer = _rememberTime;
                    _observing = true;
                    return;
                }
            }
        }
        _observing = false;
        _timer -= Time.deltaTime;
        if (_timer <= 0) _observed = false;

    }
}