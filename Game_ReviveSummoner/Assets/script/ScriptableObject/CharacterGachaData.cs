using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// キャラクター1人分の情報をまとめて持たせるためのクラス
/// </summary> 
// プロジェクトウィンドウで作成できるようにするためのアトリビュート
[CreateAssetMenu(fileName = "Data", menuName = "キャラクターのガチャデータ/CharacterData", order = 1)]
public class CharacterGachaData : ScriptableObject
{
    [Header("キャラクター名")]
    public string characterName;

    [Header("メインイラスト")]
    public Sprite pickupCharacterSprite;     

    [Header("名前イラスト")]
    public Sprite pickUpTitleSprite;         

    [Header("ミニキャラ")]
    public Sprite miniCharacterSprite;      

    [Header("魔法陣の色")]
    public Color magicCircleColor = Color.white;

    [Header("背景画像")]
    public Sprite backgroundSprite;
}
