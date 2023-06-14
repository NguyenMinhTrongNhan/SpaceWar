using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject BulletArea1;
    public GameObject BulletArea2;
    public float speed;
    void Start ()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet01 = (GameObject)Instantiate (BulletPrefab);
            bullet01.transform.position = BulletArea1.transform.position;
            GameObject bullet02 = (GameObject)Instantiate(BulletPrefab);
            bullet02.transform.position = BulletArea2.transform.position;

        }
        float x = Input.GetAxisRaw("Horizontal"); //(for left, no input, and right)
        float y = Input.GetAxisRaw("Vertical"); //(for down, no input, and up)

        Vector2 direction = new Vector2(x, y).normalized;
        //
        Move (direction);
    }
    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //
        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;
        //
        max.y = max.y - 0.285f;
        min.y = min.y - 0.285f;
        //
        Vector2 pos = transform.position;
        //
        pos += direction * speed * Time.deltaTime;
        //Make sure the new position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
}
