using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性一覧
/// </summary>
public enum CharacterAttribute
{
    Fire,
    Water,
    Wood,
    Light,
    Dark,
}

/// <summary>
/// キャラクター1人分の情報をまとめて持たせるためのクラス
/// </summary> 
// プロジェクトウィンドウで作成できるようにするためのアトリビュート
[CreateAssetMenu(fileName = "Data", menuName = "キャラクターのガチャデータ/CharacterData", order = 1)]
public class CharacterGachaData : ScriptableObject
{
    //ガチャの中身
    [Header("ピックアップキャラ")]
    public List<StateCharacter> pickupList; 
    [Header("その他のキャラ")]
    public List<StateCharacter> commonList; 

    [Header("キャラクター名")]
    public string characterName;

    [Header("属性")]
    public CharacterAttribute attribute;

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
