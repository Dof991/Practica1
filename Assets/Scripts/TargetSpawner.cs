using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private int _count = 5;
    [SerializeField] private Vector2 _massRange = new Vector2(1f, 5f);
    [SerializeField] private Vector2 _radiusRange = new Vector2(0.2f, 0.5f);
    [SerializeField] private Vector2 _spawnAreaX = new Vector2(-10, 10);
    [SerializeField] private Vector2 _spawnAreaZ = new Vector2(5, 15);
    [SerializeField] private float _horizontalSpeed = 3f;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
            SpawnTarget();
    }

    private void SpawnTarget()
    {
        float x = Random.Range(_spawnAreaX.x, _spawnAreaX.y);
        float z = Random.Range(_spawnAreaZ.x, _spawnAreaZ.y);
        Vector3 spawnPos = new Vector3(x, 1f, z);

        GameObject target = Instantiate(_targetPrefab, spawnPos, Quaternion.identity);

        float mass = Random.Range(_massRange.x, _massRange.y);
        float radius = Random.Range(_radiusRange.x, _radiusRange.y);

        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb == null) rb = target.AddComponent<Rigidbody>();

        rb.mass = mass;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.right * _horizontalSpeed;

        target.transform.localScale = Vector3.one * radius * 2f;
    }
}
