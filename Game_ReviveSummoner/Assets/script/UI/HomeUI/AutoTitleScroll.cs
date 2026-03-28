using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTitleScroll : MonoBehaviour
{
    [Header("ScrollRectをアタッチ")]
    [SerializeField] private ScrollRect scrollRect;
    [Header("スライドにかかる時間")]
    [SerializeField] private float slideDuration = 2.0f; 
    [Header(" 待機時間")]
    [SerializeField] private float waitTime = 10.0f;   
    [Header("イラストの合計枚数")]
    [SerializeField] private int totalImages = 5;      

    private int currentIndex = 0;
    private int direction = 1;

    void Start()
    {
        StartCoroutine(ScrollRoutine());
    }

/// <summary>
/// タイトルをコルーチンで動かす
/// </summary>
/// <returns></returns>
    IEnumerator ScrollRoutine()
    {
        // 画像が1枚以下の場合は動かす必要がないので終了
        if (totalImages <= 1) yield break;

        while (true)
        {
            // 10秒待機
            yield return new WaitForSeconds(waitTime);

            // 次のインデックスへ
            currentIndex += direction;

            if (currentIndex>=totalImages-1)
            {
                currentIndex = totalImages - 1;
                direction = -1;
            }
            else if (currentIndex<=0)
            {
                currentIndex = 0;
                direction = 1;
            }
            // スクロール位置の計算 (0除算防止のため totalImages - 1 で計算)
            float targetPos = 1f - ((float)currentIndex / (totalImages - 1));

            // スムーズにスライドさせる
            yield return StartCoroutine(SmoothScroll(targetPos));
        }
    }

    IEnumerator SmoothScroll(float targetPos)
    {
        float startPos = scrollRect.verticalNormalizedPosition;
        float elapsed = 0;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            // SmoothStepを使いスムーズに動かす
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(startPos, targetPos, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        scrollRect.verticalNormalizedPosition = targetPos;
    }
}
