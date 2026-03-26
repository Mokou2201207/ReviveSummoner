using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 弾が動く処理
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    [Header("弾の速度")]
    [SerializeField] private float speed = 8f; 
    [Header("画面の左端")]
    [SerializeField] private float destroyX = -12f;

    private void Update()
    {
        //左方向に移動
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        //弾が画面外にきたらオブジェクトを削除
        if (transform.position.x<destroyX)
        {
            Destroy(gameObject);
        }
    }
}
