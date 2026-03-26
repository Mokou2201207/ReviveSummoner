using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoShot : MonoBehaviour
{
    [Header("さっき作ったプレイヤーの弾")]
    [SerializeField] private GameObject bulletPrefab; 
    [Header("連射速度")]
    [SerializeField] private float fireInterval = 0.3f; 

    void Start()
    {
        // ずっと撃ち続けるループを開始
        StartCoroutine(ShotRoutine());
    }

    IEnumerator ShotRoutine()
    {
        while (true)
        {
            // 弾を生成
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // 次の弾まで待機
            yield return new WaitForSeconds(fireInterval);
        }
    }
}
