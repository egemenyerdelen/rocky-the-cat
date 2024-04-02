using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float birdSpeed;
    private Vector3 lookDirectionBird;
    // Start is called before the first frame update
    void Start()
    {
        lookDirectionBird = -transform.position.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += birdSpeed * Time.deltaTime * lookDirectionBird;

        if (transform.position.magnitude > 16 || transform.position.magnitude < -16)
        {
            Destroy(gameObject);
        }
    }
}
