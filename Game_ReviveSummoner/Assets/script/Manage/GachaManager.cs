using UnityEngine;
using UnityEngine.UI; // Imageコンポーネントを使うため
using System.Collections;
public class GachaManager : MonoBehaviour
{
    // インスペクターで設定するUI要素への参照
    [Header("メインのキャラクターのImage")]
    [SerializeField] private Image pickupCharacterImage;
    [Header("名前のタイトルのImage")]
    [SerializeField] private Image pickUpTitleImage;
    [Header("ミニキャラクターのImage")]
    [SerializeField] private Image miniCharacterImage;
    [Header("魔法陣のImage")]
    [SerializeField] private Image magicCircleImage;
    [Header("背景画像のImage")]
    [SerializeField] private Image backgroundImage;

    // ボタンが並んでいる箱
    [Header("スライドの親オブジェクト(Content)")]
    [SerializeField] private Transform contentTransform; 

    /// <summary>
    /// GachaScreenが表示されるたびに自動で呼ばれる関数
    /// </summary>
    private void OnEnable()
    {
        // そのまま即座に実行するのではなく、遅延実行をスタートさせる
        StartCoroutine(ClickFirstButtonNextFrame());
    }

    /// <summary>
    /// 1フレームだけ待ってから確実にお目当てのボタンを押す魔法の処理
    /// </summary>
    private IEnumerator ClickFirstButtonNextFrame()
    {
        // 1フレームだけ待機して、ボタン達が完全に起き上がって準備するのを待つ
        yield return null;

        // Contentが設定されていて、中に1つ以上ボタンがあるかチェック
        if (contentTransform != null && contentTransform.childCount > 0)
        {
            // 一番上の子オブジェクトからButtonコンポーネントを取得
            Button firstButton = contentTransform.GetChild(0).GetComponent<Button>();

            if (firstButton != null)
            {
                // 全ての準備が整った状態で、プログラムから強制的にクリックする
                firstButton.onClick.Invoke();
            }
        }
    }
    /// <summary>
    /// 指定されたキャラクターデータに基づいて、UIを更新する関数
    /// </summary>
    /// <param name="data">キャラクターのスプライトやカラーのデータ</param>
    public void SetCharacter(CharacterGachaData data)
    {
        if (data == null) return;

        // メインイラストの更新
        pickupCharacterImage.sprite = data.pickupCharacterSprite;

        // 名前イラストの更新
        pickUpTitleImage.sprite = data.pickUpTitleSprite;

        // ミニキャラの更新
        miniCharacterImage.sprite = data.miniCharacterSprite;

        // 背景画像の更新
        backgroundImage.sprite = data.backgroundSprite;

        // 魔法陣の「色」を更新
        magicCircleImage.color = data.magicCircleColor;
    }
}