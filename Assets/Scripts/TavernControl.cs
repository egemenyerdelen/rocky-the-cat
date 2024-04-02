using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernControl : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceWithPlayer = transform.position - playerObject.transform.position;
        if (distanceWithPlayer.magnitude < 5 && playerControllerScript.coinCounter >= 5)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<AudioSource>().Play();
                playerControllerScript.coinCounter -= 5;
                playerControllerScript.milkCounter++;
            }
        }
    }
}
