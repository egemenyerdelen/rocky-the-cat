using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [SerializeField] int cowSpeed;

    private int horizontalLimit = 16;
    //private Vector3 cowMovementAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += cowSpeed * Time.deltaTime * Vector3.left;

        if (transform.position.x < -horizontalLimit)
        {
            Destroy(gameObject);
        }
        else if(transform.position.x > horizontalLimit)
        {
            Destroy(gameObject);
        }
    }
}
