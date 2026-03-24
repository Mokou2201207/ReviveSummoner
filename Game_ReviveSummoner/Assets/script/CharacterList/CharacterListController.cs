using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// 今もってるキャラクターの種類だけの処理（重複不可）※所持してるキャラクターのデータはCollectManagerに入ってます。
/// </summary>
public class CharacterListController : MonoBehaviour
{
    [Header("キャラクター一覧のprefab")]
    [SerializeField] private GameObject cardPrefab;
    [Header("ScrollViewのContentにアタッチ")]
    [SerializeField] private Transform contentTransform;
    [Header("キャラクター一覧のパネル")]
    [SerializeField] private GameObject characterListPanal;

    // このパネルが表示された瞬間に実行される
    private void OnEnable()
    {
        RefreshList();
    }

    /// <summary>
    /// 同じキャラクターがかぶったときの表示は同じにならない用処理
    /// </summary>
    public void RefreshList()
    {
        //すでに並んでいる古いカードを全部消す
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        //CollectionManagerからリストを取得し、重複を除外する
        List<StateCharacter> uniqueCharacters = CollectManager.Instance.ownedCharacters
             //かぶってるものを除外
             .Distinct()
             //リストとして保存
             .ToList();

        //キャラクターの数だけプレハブを生成してデータを流し込む
        foreach (StateCharacter charData in uniqueCharacters)
        {
            //インスタンス化
            GameObject newCard = Instantiate(cardPrefab, contentTransform);

            //カードのスクリプトを取得してデータをセット
            CharacterCardUI cardUI = newCard.GetComponent<CharacterCardUI>();
            if (cardUI != null)
            {
                cardUI.SetCharacterData(charData);
            }
        }
    }

    /// <summary>
    /// このパネルを表示
    /// </summary>
    public void OnControllerList()
    {
        if (characterListPanal == null) return;
        characterListPanal.SetActive(true);
    }

    /// <summary>
    /// このパネルを表示
    /// </summary>
    public void OffControllerList()
    {
        if (characterListPanal == null) return;
        characterListPanal.SetActive(false);
    }
}
