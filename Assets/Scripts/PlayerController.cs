using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 30;
    float screenHalfWidthInWorldUnit;
    public event System.Action OnPlayerDeath;
    // Start is called before the first frame update
    void Start()
    {
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnit = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float velocity = inputX * speed;

        transform.Translate(Vector2.right * velocity * Time.deltaTime);
        if(transform.position.x < -screenHalfWidthInWorldUnit)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnit, transform.position.y);
        } 
        if(transform.position.x > screenHalfWidthInWorldUnit)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnit, transform.position.y);
        }

    }

    void OnTriggerEnter2D (Collider2D triggerCollider)
    {
        if(triggerCollider.tag == "Falling Object")
        {
            if(OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }
}
