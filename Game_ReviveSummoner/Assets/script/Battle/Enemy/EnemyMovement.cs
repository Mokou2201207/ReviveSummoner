using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の移動処理
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("ラインの設定")]
    [SerializeField] private float[] laneYPositions = { 2.7f, -0.7f, -2.7f };

    //動きのバリエーション
    [Header("爆速移動の時間")]
    [Range(0.1f, 2.0f)] public float fastDuration = 0.15f;
    [Header("ゆっくり移動の時間")]
    [Range(0.6f, 5.0f)] public float slowDuration = 1.2f;
    [Header("最大停止時間")]
    [SerializeField] private float maxWaitTime = 1.5f;

    private float targetY;

    private void Start()
    {
        // 最初の位置をセット
        float startY = laneYPositions[Random.Range(0, laneYPositions.Length)];
        transform.position = new Vector3(transform.position.x, startY, 0);

        // 常に動き続けるルーチンを開始
        StartCoroutine(ErraticMoveRoutine());
    }

    IEnumerator ErraticMoveRoutine()
    {
        while (true)
        {
            //確率
            float dice = Random.value;

            float duration;
            float waitTime;

            //確率45パーセントで爆速
            if (dice < 0.45f)
            {
                duration = fastDuration;
                waitTime = Random.Range(0.1f, 0.3f);
            }
            //確率45パーセントでゆっくり
            else if (dice < 0.90f)
            {
                duration = slowDuration;
                waitTime = Random.Range(0.5f, maxWaitTime);
            }
            //確率10パーセントで停止
            else
            {
                duration = 0f;
                waitTime = Random.Range(0.8f, 2.0f);
            }

            if (duration > 0)
            {
                //どこのラインに行くかランダムでポジションを決める
                int targetIndex = Random.Range(0, laneYPositions.Length);
                float targetY = laneYPositions[targetIndex];

                //時間リセット用
                float elapsed = 0f;

                Vector3 startPos = transform.position;
                Vector3 endPos = new Vector3(transform.position.x, targetY, 0);

                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    float t = elapsed / duration;

                    //急発進や急停止
                    float smoothedT = Mathf.SmoothStep(0f, 1f, t);
                    transform.position = Vector3.Lerp(startPos, endPos, smoothedT);
                    yield return null;
                }
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}



