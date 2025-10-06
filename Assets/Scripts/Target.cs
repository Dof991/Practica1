using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<QuadriicDrag>())
        {
            Debug.Log("Попадание в мишень!");
            Destroy(gameObject);
        }
    }
}
