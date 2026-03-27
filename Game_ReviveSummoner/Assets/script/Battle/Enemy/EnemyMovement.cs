using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の移動処理
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("上下の幅")]
    public float amplitude = 2.0f; 
    [Header("移動の速さ")]
    public float frequency = 1.0f; 

    private float startY;

    private void Start()
    {
        //最初の位置を記録
        startY = transform.position.y;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Update()
    {
        //時間に合わせてY座標を計算
        float newY=startY+Mathf.Sin(Time.time*frequency)*amplitude;

        //現在のX座標を維持したままY座標だけ更新
        transform.position=new Vector3(transform.position.x,newY,0);
    }
}
