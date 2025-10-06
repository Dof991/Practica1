using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TraectoryRender))]
public class BalistCalc : MonoBehaviour
{
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private float _muzzleVelocity = 20;
    [SerializeField, Range(0, 85)] private float _muzzleAngle = 20;

    [Space]
    [SerializeField] private QuadriicDrag _projectilePrefab;

    [Header("Диапазоны параметров")]
    [SerializeField] private float _minMass = 0.5f;
    [SerializeField] private float _maxMass = 2.5f;
    [SerializeField] private float _minRadius = 0.05f;
    [SerializeField] private float _maxRadius = 0.25f;

    [Header("Физика воздуха")]
    [SerializeField] private float _dragCoef = 0.47f;
    [SerializeField] private float _airDensity = 1.225f;
    [SerializeField] private Vector3 _wind = Vector3.zero;

   
    [SerializeField] private float _smoothTime = 1f;

    private TraectoryRender _trajectoryRenderer;
    private float _mass;
    private float _radius;
    private float _targetMass;
    private float _targetRadius;
    private float _massVelocity;
    private float _radiusVelocity;

    private void Start()
    {
        _trajectoryRenderer = GetComponent<TraectoryRender>();
        GenerateNewTargetParameters(); 
    }

    private void Update()
    {
        if (_launchPoint == null) return;

        
        _mass = Mathf.SmoothDamp(_mass, _targetMass, ref _massVelocity, _smoothTime);
        _radius = Mathf.SmoothDamp(_radius, _targetRadius, ref _radiusVelocity, _smoothTime);

        
        if (Mathf.Abs(_mass - _targetMass) < 0.01f && Mathf.Abs(_radius - _targetRadius) < 0.001f)
        {
            GenerateNewTargetParameters();
        }

        Vector3 v0 = CalculateVelocityVector(_muzzleAngle);

        
        _trajectoryRenderer.DrawWithAirEuler(_launchPoint.position, v0, _mass, _radius, _dragCoef, _airDensity, _wind);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Fire(v0);
    }

    private void GenerateNewTargetParameters()
    {
        _targetMass = Random.Range(_minMass, _maxMass);
        _targetRadius = Random.Range(_minRadius, _maxRadius);
    }

    private void Fire(Vector3 initialVelocity)
    {
        if (_projectilePrefab == null) return;

        GameObject newProjectile = Instantiate(_projectilePrefab.gameObject, _launchPoint.position, Quaternion.identity);
        QuadriicDrag drag = newProjectile.GetComponent<QuadriicDrag>();
        drag.SetPhysicalParams(_mass, _radius, _dragCoef, _airDensity, _wind, initialVelocity);
    }

    private Vector3 CalculateVelocityVector(float angle)
    {
        float vx = _muzzleVelocity * Mathf.Cos(angle * Mathf.Deg2Rad);
        float vy = _muzzleVelocity * Mathf.Sin(angle * Mathf.Deg2Rad);
        return _launchPoint.forward * vx + _launchPoint.up * vy;
    }
}
