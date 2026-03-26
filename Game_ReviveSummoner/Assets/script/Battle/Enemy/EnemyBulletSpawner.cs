using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    [Header("プレハブ")]
    [SerializeField] private GameObject bulletPrefab;
    [Header("何秒おきに撃つか")]
    [SerializeField] private float spawnInterval = 1.0f;

    //プレイヤーのコードと同じ座標に合わせるのがコツ！
    [SerializeField] private float[] laneYPositions = { 2.7f, -0.7f, -2.7f };
    // 画面の右端
    [SerializeField] private float spawnX = 10f; 

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 弾を撃ち続けるループを開始
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // インターバル分待つ
            yield return new WaitForSeconds(spawnInterval);

            // ランダムにラインを1つ選ぶ (0, 1, 2)
            int randomLane = Random.Range(0, laneYPositions.Length);

            // 弾を生成！
            SpawnBullet(randomLane);
        }
    }

    void SpawnBullet(int laneIndex)
    {
        Vector3 spawnPos = new Vector3(spawnX, laneYPositions[laneIndex], 0);
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
    }
}
