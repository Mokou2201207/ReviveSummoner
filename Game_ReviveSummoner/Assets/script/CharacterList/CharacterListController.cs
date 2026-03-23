using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void RefreshList()
    {
        //すでに並んでいる古いカードを全部消す
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        //CollectionManagerから所持キャラリストを取得
        List<StateCharacter> myCharacters = CollectManager.Instance.ownedCharacters;

        //キャラクターの数だけプレハブを生成してデータを流し込む
        foreach (StateCharacter charData in myCharacters)
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
