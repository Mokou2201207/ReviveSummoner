using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleLineShot : MonoBehaviour
{
    [Header("弾のprefab")]
    [SerializeField] private GameObject bulletPrefab;
    [Header("攻撃するline")]
    [SerializeField] private float[] laneYPositions = { 2.7f, -0.7f, -2.7f };

    public void Execute()
    {
        // 3本中「撃たないライン」を1つランダムに選ぶ
        int safeLane = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            if (i == safeLane) continue; // 安全なラインは飛ばす

            Vector3 spawnPos = new Vector3(10f, laneYPositions[i], 0);
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        }
    }
}
