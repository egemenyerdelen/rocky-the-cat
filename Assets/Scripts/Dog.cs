using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] float dogSpeed;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyMoveDirection = (playerObject.transform.position - transform.position).normalized;
        if (!(transform.position.y < 3))
        {
            transform.position += dogSpeed * Time.deltaTime * enemyMoveDirection;
        }
    }
}
