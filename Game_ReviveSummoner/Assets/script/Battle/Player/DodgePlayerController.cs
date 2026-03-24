using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgePlayerController : MonoBehaviour
{
    [Header("コンポーネント")]
    [SerializeField]private Animator anim;

    //ラインの座標
    [Header("ラインの設定")]
    [SerializeField] private float[] laneYPositions = { 2.7f, -0.7f, -2.7f };

    [Header("今どこのラインにいるか")]
    private int currentLaneIndex = 1;

    [Header("どれくらい指を動かしたらフリック判定されるか")]
    [SerializeField] private float flickThreshold = 50f;

    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;

    private void Start()
    {
        anim=GetComponentInChildren<Animator>();
    }
    /// <summary>
    /// タッチ判定
    /// </summary>
    private void Update()
    {
        // 指を押した瞬間
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPosition = Input.mousePosition;
        }

        // 指を離した瞬間
        if (Input.GetMouseButtonUp(0))
        {
            touchEndPosition = Input.mousePosition;
            //はなした瞬間Playが動く処理へ
            HandleFlick();
        }
    }

    /// <summary>
    /// 移動判定
    /// </summary>
    void HandleFlick()
    {
        float verticalDistance = touchEndPosition.y - touchStartPosition.y;

        // 上にフリックした場合
        if (verticalDistance > flickThreshold)
        {
            MoveLane(-1); // インデックスを減らす（上へ）
        }
        // 下にフリックした場合
        else if (verticalDistance < -flickThreshold)
        {
            MoveLane(1); // インデックスを増やす（下へ）
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="direction"></param>
    void MoveLane(int direction)
    {
        int nextLane = Mathf.Clamp(currentLaneIndex + direction, 0, laneYPositions.Length - 1);

        if (nextLane != currentLaneIndex)
        {
            currentLaneIndex = nextLane;

            Vector3 newPos = transform.position;
            newPos.y = laneYPositions[currentLaneIndex];
            transform.position = newPos;

            // アニメーション開始
            StartCoroutine(PlayMoveAnimation());
        }
    }

    IEnumerator PlayMoveAnimation()
    {
        // 移動中は移動アニメション
        anim.SetInteger("State", 5);

        // アニメーションが切り替わるまで少し待つ
        yield return new WaitForSeconds(0.1f);

        // 止まっているときはIdleアニメション
        anim.SetInteger("State", 1);
    }
}
