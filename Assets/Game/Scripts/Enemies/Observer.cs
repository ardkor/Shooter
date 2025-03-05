using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] Collider _target;
    [SerializeField] Transform _rayTarget;

    [SerializeField] LayerMask aimColliderLayerMask;

    [SerializeField] private float _rayLength;

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
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, _target.bounds))
        {
            Ray head_ray = new Ray(transform.position, (_rayTarget.position - transform.position).normalized);
            Debug.DrawRay(head_ray.origin, head_ray.direction * 100f, Color.blue);
            if (Physics.Raycast(head_ray, out RaycastHit hit, _rayLength, aimColliderLayerMask))
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