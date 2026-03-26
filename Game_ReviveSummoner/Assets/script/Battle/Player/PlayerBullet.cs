using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("プレイヤーの弾の速度")]
    [SerializeField] private float speed = 12f; 
    [Header("画面の右端で消える")]
    [SerializeField] private float destroyX = 12f; 

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // 画面外に出たら消える
        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }

    // 敵に当たった時の処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 敵にダメージを与える
            Debug.Log("敵に命中！");
            Destroy(gameObject);
        }
    }
}