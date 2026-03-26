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

    /// <summary>
    /// プレイヤーの弾が敵に当たったときの際の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //当たったオブジェクトからEnemyHpのスクリプトを探す
           EnemyHp enumy = collision.GetComponent<EnemyHp>();

            if (enumy!=null)
            {
                //10ダメージを与える
                enumy.Damage(10f);
            }

            //玉を削除
            Destroy(gameObject);
        }
    }
}