using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// レアリティ一覧
/// </summary>
public enum CharacterRarity
{
    Star1,
    Star2,
    Star3,
    Star4,
}

/// <summary>
/// キャラクターのスキル一覧
/// </summary>
public enum CharacterSkills
{
    //星１
    Defense,// 挑発（防御UPなし）

    //星２
    SmallHeel,//回復（小）

    //星３
    BigHeel,//回復大
    Hide,//隠れ身
    DeathZone,//ダメージ割合
    Hitrate,//命中率

    //星４
    Shield,//ダメージ無効
}

/// <summary>
/// キャラクターのステータス
/// </summary>
[CreateAssetMenu(fileName = "Character", menuName = "キャラクターのステータス/CharacterData")]
public class StateCharacter : ScriptableObject
{
    [Header("キャラクターの名前")]
    public string characterName;
    [Header("レアリティ")]
    public CharacterRarity rarity;
    [Header("属性")]
    public CharacterAttribute attribute; 

    [Header("体力")]
    public int hp;
    [Header("攻撃")]
    public int attack;
    [Header("スキル")]
    public CharacterSkills skill;

    [Header("キャラクターの立ち絵")]
    public Sprite CharacterSprite;
    [Header("ガチャ結果一覧で使う小さい顔アイコン")]
    public Sprite iconSprite;     

}
