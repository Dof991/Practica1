using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class QuadriicDrag : MonoBehaviour
{
    private float _mass;
    private float _radius;
    private float _dragCoef;
    private float _airDensity;
    private Vector3 _wind;

    private Rigidbody _rb;
    private float _area;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 vRel = _rb.linearVelocity - _wind;
        float speed = vRel.magnitude;

        if (speed < 1e-6f) return;

        Vector3 drag = -0.5f * _airDensity * _dragCoef * _area * speed * vRel;
        _rb.AddForce(drag, ForceMode.Force);
    }

    public void SetPhysicalParams(float mass, float radius, float dragCoef, float airDensity, Vector3 wind, Vector3 initialVelocity)
    {
        _mass = mass;
        _radius = radius;
        _dragCoef = dragCoef;
        _airDensity = airDensity;
        _wind = wind;

        _rb.mass = _mass;
        _rb.useGravity = true;
        _rb.linearVelocity = initialVelocity;
        _area = Mathf.PI * _radius * _radius;
    }
}
