using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 2f;
    public Vector3 cowLastPosition;
    private GameManager gameManagerScript;
    [SerializeField] GameObject coinPrefab;

    float timeToDestroy = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
        gameManagerScript = FindObjectOfType<GameManager>();

        timeToDestroy += Time.deltaTime;

        if (timeToDestroy >= 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Transfer this code lines to bullet while using bullet mechanics 
        if (collision.CompareTag("Cow"))
        {
            int randomValue = Random.Range(0, 100);
            cowLastPosition = collision.transform.position;
            if (randomValue > 50)
            {
                gameManagerScript.MilkSpawner();
            }
            Destroy(collision.gameObject);
            
            StartCoroutine(DestroyBullet());
        }
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(DestroyBullet());

            Instantiate(coinPrefab, collision.transform.position, Quaternion.identity);
        }
    }

    IEnumerator DestroyBullet()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
