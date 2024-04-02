using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDizzy : MonoBehaviour
{
    [SerializeField] GameObject cross;
    [SerializeField] Slider slider;
    [SerializeField] float dizzyMeter = 0f;
    [SerializeField] float maxDizzyMeter = 10f;
    public float DizzyMeter { get { return dizzyMeter; } }

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        dizzyMeter = playerController.milkCounter;
        slider.value = dizzyMeter;
        slider.maxValue = maxDizzyMeter;

        if (dizzyMeter == maxDizzyMeter)
        {
            playerController.ResetMilkCount();
        }
    }
}
