using UnityEngine;
using UnityEngine.InputSystem;


public class CannonController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 60f;

    private void Update()
    {
        Vector3 move = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            move += transform.forward;
        if (Keyboard.current.sKey.isPressed)
            move -= transform.forward;
        if (Keyboard.current.aKey.isPressed)
            move -= transform.right;
        if (Keyboard.current.dKey.isPressed)
            move += transform.right;

        if (move.sqrMagnitude > 0.01f)
        {
            move.y = 0; 
            transform.position += move.normalized * moveSpeed * Time.deltaTime;
        }

        if (Keyboard.current.qKey.isPressed)
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        if (Keyboard.current.eKey.isPressed)
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
