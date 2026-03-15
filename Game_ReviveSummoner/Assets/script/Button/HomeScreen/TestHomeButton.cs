using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHomeButton : MonoBehaviour
{
    [Header("ガチャ画面（GachaPanel）")]
    [SerializeField] private GameObject gachaPanel;

    [Header("ローディングスクリーン（LoadingScreenPanel）")]
    [SerializeField] private GameObject loadingScreen;

    [Header("最初のキャラクターデータ（Toyohimeなど）")]
    [SerializeField] private CharacterGachaData firstCharacterData;

    [SerializeField]private GachaManager gachaManager;

    private void Start()
    {
        if (gachaPanel != null)
        {
            gachaManager = gachaPanel.GetComponent<GachaManager>();
        }
    }

    public void OnButtonClicked()
    {
        Debug.Log("ホーム画面のガチャボタンが押されました！");

        // 1. ローディングスクリーンを表示
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        // 2. ガチャパネルを表示
        if (gachaPanel != null)
        {
            gachaPanel.SetActive(true);
        }

        // 🚨 原因究明のためのチェック！
        if (gachaManager == null)
        {
            Debug.LogError("【エラー】GachaManagerが見つかりません！ガチャパネルがセットされていないかも？");
            return; // ここで処理がストップします
        }

        if (firstCharacterData == null)
        {
            Debug.LogError("【エラー】最初のキャラクターデータがセットされていません！インスペクターを確認してください！");
            return; // ここで処理がストップします
        }

        Debug.Log("準備OK！GachaManagerに処理をバトンタッチしてコルーチンを開始します！");

        // 💡 超重要修正：gachaManager.StartCoroutine に変更！
        // これにより、ホーム画面が非表示になってもガチャ画面側で確実に処理が続きます！
        gachaManager.StartCoroutine(gachaManager.LoadAssetsAndSetup(firstCharacterData));
    }
}