using UnityEngine;
using UnityEngine.Serialization;

public class GroundLogic : MonoBehaviour
{
    public bool isGrounded;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))  
        {
            isGrounded = true;
            Debug.Log("Player tocando el suelo");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))  
        {
            isGrounded = false;
            Debug.Log("Player no está en el suelo");
        }
    }
}
