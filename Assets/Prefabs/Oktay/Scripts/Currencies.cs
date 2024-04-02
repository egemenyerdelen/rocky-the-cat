using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currencies : MonoBehaviour
{
    float coin;
    float milk;

    public void CollectCoin(float collectedAmount)
    {
        coin += collectedAmount;
    }

    private void CollectMilk()
    {
        milk += 1;
    }
}
