using System.Collections;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    float speed;
    void Start()
    {
        speed = 5f;
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
}
