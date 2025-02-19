using UnityEngine;
using UnityEngine.Android;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("<color=orange>Values</color>")]
    [Tooltip("Modifies how fast the player will move.")]
    [SerializeField] private float _movSpeed = 5f;
    public Transform orientation;

    public ParticleSystem ripple;

    private float _xAxis = 0f, _zAxis = 0f;

    private Vector3 _dir = new();

    private Rigidbody _rb;

    public static Player instance;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.angularDrag = 1f;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        instance = this;

    }

    private void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (Dialogue.Instance.importantTextOnDisplay) return;
        if (PlayerInventory.Instance.isInventoryActive) return;
        if (PlayerInventory.Instance.isInspectActive) return;
        if ((_xAxis != 0 || _zAxis != 0))
        {
            Movement(_xAxis, _zAxis);
            SpeedControl();
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        //Debug.Log(flatVel.magnitude);

        if(flatVel.magnitude > _movSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _movSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    private void Movement(float xAxis, float zAxis)
    {
        _dir = (orientation.forward * zAxis + orientation.right * xAxis).normalized;

        _rb.AddForce(_dir * _movSpeed * 10f, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            ripple.Play();
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    var emission = ripple.emission;
    //    if (other.gameObject.layer == 4)
    //    {
    //        if((_xAxis != 0 || _zAxis != 0))
    //        {
    //            var higher = Mathf.Abs(_zAxis) > Mathf.Abs(_xAxis) ? _zAxis : _xAxis;
    //            emission.rateOverTime = Mathf.Abs(higher) * _movSpeed;
    //        }
    //        else
    //        {
    //            emission.rateOverTime = 0f;
    //        }
    //    }
    //}
    
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == 4)
    //    {
    //        ripple.Stop();
    //    }
    //}
}
