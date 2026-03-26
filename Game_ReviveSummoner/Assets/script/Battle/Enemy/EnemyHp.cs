using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 敵のHP
/// </summary>
public class EnemyHp : MonoBehaviour
{
    [Header("HPバー")]
    [SerializeField] private Slider hpSlider;

    [Header("敵のHP")]
    [SerializeField] private float maxHP = 100f;
    private float currentHP;

    [Header("強制的にコマンドに戻される値")]
    [SerializeField] private float finishThreshold = 10f;

    /// <summary>
    /// 開始
    /// </summary>
    private void Start()
    {
       currentHP=maxHP;

        //Sliderの初期設定
        if (hpSlider!=null)
        {
            hpSlider.maxValue=maxHP;
            hpSlider.value=maxHP;
        }
    }
    
    /// <summary>
    /// ダメージを受ける関数
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(float amount)
    {
        //HPを減らす
        currentHP-=amount;

        // ダメージに合わせてスライダーを更新
        if (hpSlider != null)
        {
            // 💡 ログに「スライダーがついているオブジェクトの名前」を出してみる
            Debug.Log("動かそうとしているスライダーの名前: " + hpSlider.gameObject.name);
            hpSlider.value = currentHP;
        }
        else
        {
            Debug.LogError("hpSlider自体が空っぽです！");
        }

        //HPがミリになったらコマンドへ
        if (currentHP<=finishThreshold)
        {
            TriggerFinishChance();
        }
    }

    /// <summary>
    /// 避ける画面からコマンドの画面へ
    /// </summary>
    void TriggerFinishChance()
    {

    }
}
