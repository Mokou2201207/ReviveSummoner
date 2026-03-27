using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 各パターンのスクリプトを個別に参照します
    private SingleLineShot pattern1;
    private DoubleLineShot pattern2; // 2ライン攻撃（仮）
    

    void Awake()
    {
        pattern1 = GetComponent<SingleLineShot>();
        pattern2 = GetComponent<DoubleLineShot>();
       
    }


        void Start()
        {
            // 💡 テスト用：ゲーム開始と同時に攻撃フェーズを始める
            StartAttackPhase();
        }
    

    public void StartAttackPhase()
    {
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        // --- パターン1 ---
        yield return new WaitForSeconds(1.5f);
        if (pattern1 != null) pattern1.Execte();

        // --- パターン2 ---
        yield return new WaitForSeconds(2.0f); // 次の攻撃までの間隔
        if (pattern2 != null) pattern2.Execute();

        // 全て終わったら終了処理へ
        yield return new WaitForSeconds(1.5f);
        EndDodgePhase();
    }

    void EndDodgePhase()
    {
        Debug.Log("全3パターンの攻撃が終了。コマンド画面へ戻ります。");
        // ここで「画面スライド」の処理を呼ぶ
    }
}
