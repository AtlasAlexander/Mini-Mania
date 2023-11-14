using UnityEngine;

public class Scale : MonoBehaviour
{
    public Rigidbody scaleRb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weight"))
        {
            {
                Debug.Log("Collision Detected");
                scaleRb.isKinematic = false;
            }
        }
    }
}
