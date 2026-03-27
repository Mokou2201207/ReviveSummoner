using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 三つラインの中にランダムに攻撃
/// </summary>
public class SingleLineShot : MonoBehaviour
{
    [Header("弾のプレハブ")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("ラインの高さ設定")]
    [SerializeField] private float[] laneYPositions = { 2.7f, -0.7f, -2.7f };

    [Header("発射位置のX座標")]
    [SerializeField] private float spawnX = 10f;

    /// <summary>
    ///弾をランダムに発射する。
    /// </summary>
    public void Execte()
    {
        // 0, 1, 2 の中からランダムに1つ選ぶ
        int randomLane = Random.Range(0, laneYPositions.Length);

        //発射位置を決定
        Vector3 spawnPos = new Vector3(spawnX, laneYPositions[randomLane], 0);

        // 弾を生成
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
    }
}
