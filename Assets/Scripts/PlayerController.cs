using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;
    public int milkCounter;
    public int coinCounter;
    public Vector3 cowLastPosition;

    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject birdPrefab;
    private GameManager gameManagerScript;
    private float borderLimitX = 15;
    private float borderLimitY = 10;
    private float horizontalInput;
    private float verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        milkCounter = 0;
        coinCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            milkCounter++;
        }
        MovementAndLimit();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Milk"))
        {
            StartCoroutine(DrinkMilk(other));
            milkCounter++;
        }

        else if (other.CompareTag("Coin"))
        {
            StartCoroutine(CollectCoin(other));
            coinCounter++;
        }

        //Transfer this code lines to bullet while using bullet mechanics
        //if (other.CompareTag("Cow"))
        //{
        //    int randomValue = Random.Range(0, 100);
        //    cowLastPosition = other.transform.position;
        //    if (randomValue > 50)
        //    {
        //        gameManagerScript.MilkSpawner();
        //    }
        //    Destroy(other.gameObject);
        //}
        //else if (other.CompareTag("Enemy"))
        //{
        //    Destroy(other.gameObject);
        //    Instantiate(coinPrefab, other.transform.position, Quaternion.identity);
        //}
    }

    IEnumerator DrinkMilk(Collider2D other)
    {
        other.gameObject.GetComponent<AudioSource>().Play();
        other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        other.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(other.gameObject);
    }

    IEnumerator CollectCoin(Collider2D other)
    {
        other.gameObject.GetComponent<AudioSource>().Play();
        other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(other.gameObject);
    }

    public void MovementAndLimit()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0) * playerSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -borderLimitX, borderLimitX);
        newPosition.y = Mathf.Clamp(newPosition.y, -borderLimitY, borderLimitY);

        transform.position = newPosition;
    }

    public void BirdDistraction()
    {
        Vector3 lookToBird = transform.position - birdPrefab.transform.position;
    }

    public void ResetMilkCount()
    {
        milkCounter = 0;
    }
}
