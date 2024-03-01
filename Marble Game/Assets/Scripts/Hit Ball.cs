using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HitBall : MonoBehaviour
{
    public GameObject marbleCamera;

    Rigidbody rb;
    public float hitMultiplier = 1f;
    float hitForce = 0;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (hitForce < 3)
            {
                hitForce += Time.deltaTime;
            }
            Debug.Log(hitForce);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hitForce > 0.2)
            {
                direction = new Vector3(marbleCamera.transform.forward.x, 0, marbleCamera.transform.forward.z);
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.AddForce(direction * (hitForce * hitMultiplier), ForceMode.Impulse);
            }
            hitForce = 0;
        }
    }
}
