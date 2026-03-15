using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ガチャ一覧を押した際の処理
/// </summary>
[RequireComponent(typeof(Button))]
public class GachaButton : MonoBehaviour
{
    [Header("このボタンに対応するキャラクターデータ")]
    [SerializeField] private CharacterGachaData characterData;
    [Header("GachaManagerへの参照")]
    [SerializeField] private GachaManager gachaManager;

    private void Awake()
    {
        // 自分のオブジェクトについているButtonコンポーネントを取得
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        // マネージャーに、自分のキャラクターデータを渡して更新してもらう
        if (gachaManager != null && characterData != null)
        {
            gachaManager.SetCharacter(characterData);
        }
    }
}