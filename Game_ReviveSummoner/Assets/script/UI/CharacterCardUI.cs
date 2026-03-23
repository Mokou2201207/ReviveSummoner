using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクター一覧の差し替え処理
/// </summary>
public class CharacterCardUI : MonoBehaviour
{
    [Header("キャラクターの画像")]
    [SerializeField] private Image characterImage;
    [Header("名前Text")]
    [SerializeField] private TextMeshProUGUI nameText;
    [Header("レベルText")]
    [SerializeField] private TextMeshProUGUI levelText;
    [Header("属性表示の画像")]
    [SerializeField] private Image attributeColorImage;

    [Header("赤色")]
    [SerializeField] private Color fireColor = new Color(1f, 0.3f, 0.3f); 
    [Header("青系")]
    [SerializeField] private Color waterColor = new Color(0.3f, 0.5f, 1f);
    [Header("緑系")]
    [SerializeField] private Color woodColor = new Color(0.3f, 1f, 0.3f); 
    [Header("黄・白系")]
    [SerializeField] private Color lightColor = new Color(1f, 1f, 0.5f); 
    [Header("紫系")]
    [SerializeField] private Color darkColor = new Color(0.7f, 0.3f, 1f);  

    /// <summary>
    /// キャラクター一覧の内容を変更する処理
    /// </summary>
    /// <param name="data"></param>
    public void SetCharacterData(StateCharacter data)
    {
        if (data == null) return;

        //キャラクタの画像をセット
        characterImage.sprite = data.CharacterSprite;

        // 名前をセット
        nameText.text = data.characterName;

        // レベルをセット
        levelText.text = "Lv. 1";

        // 属性に合わせてImageの色を変える
        if (attributeColorImage != null)
        {
            attributeColorImage.color = GetAttributeColor(data.attribute);
        }
    }

    /// <summary>
    /// 属性に応じて色を替える
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    private Color GetAttributeColor(CharacterAttribute attribute)
    {
        switch (attribute)
        {
            case CharacterAttribute.Fire: return fireColor;
            case CharacterAttribute.Water: return waterColor;
            case CharacterAttribute.Wood: return woodColor;
            case CharacterAttribute.Light: return lightColor;
            case CharacterAttribute.Dark: return darkColor;
            default: return Color.white;
        }
    }
}

