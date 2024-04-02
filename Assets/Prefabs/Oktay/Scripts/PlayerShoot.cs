using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float rpm = 60;
    [SerializeField] GameObject arm;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullet;
    
    float timeSinceLastShot;

    MouseLook mouseLook;

    // Start is called before the first frame update
    void Start()
    {
        mouseLook = GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastShot < 1f / (rpm / 60f))
        {
            timeSinceLastShot += Time.deltaTime;
        }

        arm.transform.up = new Vector3(mouseLook.crossPos.x - gun.transform.position.x, mouseLook.crossPos.y - gun.transform.position.y, 0);
        

        if (Input.GetButtonDown("Fire1") && CanShoot())
        {
            Debug.Log("Atesledim"); 
            timeSinceLastShot = 0f;
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        }
    }

    private bool CanShoot() => timeSinceLastShot > 1f / (rpm / 60f);
}
