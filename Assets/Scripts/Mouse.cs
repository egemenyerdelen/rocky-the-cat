using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] float mouseSpeed;
    [SerializeField] int bottomBorderLimit = -22;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += mouseSpeed * Time.deltaTime * Vector3.down;

        if (transform.position.y < bottomBorderLimit)
        {
            Destroy(gameObject);
        }
    }
}
