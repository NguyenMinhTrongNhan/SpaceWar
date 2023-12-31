﻿using System.Collections;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    GameObject scoreUIText;
    float speed;
    public GameObject Explosion;
    void Start()
    {
        speed = 5f;
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreText");

    }
    private void Update()
    {
        Vector2 position = transform.position;
        //
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        //
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //
        if ((col.tag == "Player") || (col.tag == "PlayerBullet"))
        {
            scoreUIText.GetComponent<GameScore>().Score += 1000;
            StartExplosion();
            Destroy(gameObject);
        }
    }
    void StartExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}
