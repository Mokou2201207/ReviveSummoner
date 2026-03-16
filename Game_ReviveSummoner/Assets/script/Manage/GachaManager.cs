using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
public class GachaManager : MonoBehaviour
{
    [Header("所持石のテキストUI")]
    [SerializeField] private TextMeshProUGUI gemCountText;

    //今持ってるクリスタル
    private int currentGems = 90000;

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

    [Header("BGM設定")]
    [Header("AudioSourceを参照")]
    [SerializeField] private AudioSource bgmAudioSource;
    [Header("火属性の曲")]
    [SerializeField] private AudioClip fireBgm;
    [Header("水属性の曲")]
    [SerializeField] private AudioClip waterBgm;
    [Header("木属性の曲")]
    [SerializeField] private AudioClip woodBgm;
    [Header("光属性の曲")]
    [SerializeField] private AudioClip lightBgm;
    [Header("闇属性の曲")]
    [SerializeField] private AudioClip darkBgm;

    //フェード設定
    [Header("フェードにかける時間")]
    [SerializeField, Range(0.1f, 3.0f)] private float fadeDuration = 1.0f;
    [Header("BGMの最大音量")]
    [SerializeField, Range(0.0f, 1.0f)] private float maxVolume = 1.0f;

    [Header("ローディングスクリーン")]
    [SerializeField] private GameObject loadingScreen;

    // 前に選んだ属性を覚えておくための変数
    private CharacterAttribute currentAttribute;
    private bool isFirstPlay = true;
    private Coroutine fadeCoroutine;

    // 今アクティブなガチャのラインナップを一時保存する変数
    [SerializeField]private List<StateCharacter> currentPickupPool = new List<StateCharacter>();
    [SerializeField] private List<StateCharacter> currentCommonPool = new List<StateCharacter>();

    /// <summary>
    /// クリスタルのUIの更新
    /// </summary>
    private void Start()
    {
        UpdateGemUI();
    }

    /// <summary>
    /// クリスタルのTEXTを更新
    /// </summary>
    private void UpdateGemUI()
    {
        if (gemCountText != null)
        {
            gemCountText.text = currentGems.ToString();
        }
    }

    /// <summary>
    /// ガチャを一回引く際のボタンの処理
    /// </summary>
    public void DrawSingleGacha()
    {
        //石が足りているか
        if (currentGems < 300) return;

        //テキストのUIを更新
        currentGems -= 300;
        UpdateGemUI();

        StateCharacter result = null;
        int chance = Random.Range(0, 100); // 0〜99のサイコロを振る

        //  ピックアップの確率３パーセント
        if (chance < 3 && currentPickupPool.Count > 0)
        {
            // ピックアップリストから選ぶ
            result = currentPickupPool[Random.Range(0, currentPickupPool.Count)];
            Debug.Log("★ピックアップ確定演出！★");
        }
        //星2の確率３５パーセント
        else if (chance<38 &&currentCommonPool.Count > 0)
        {
            // 雑魚リストから選ぶ
            result = GetRandomCharacterByRarity(currentCommonPool, CharacterRarity.Star2);
        }
        //星１のキャラ確定
        else if (currentCommonPool.Count > 0)
        {
            // 雑魚リストから選ぶ
            result = GetRandomCharacterByRarity(currentCommonPool, CharacterRarity.Star1);
        }

        if (result != null) Debug.Log("ガチャ結果: " + result.characterName);
    }

    /// <summary>
    /// ガチャを10回連続で引くボタンの処理
    /// </summary>
    public void DrawTenGacha()
    {
        // 石が足りているか
        if (currentGems < 3000)
        {
            Debug.Log("クリスタルが足りません");
            return;
        }

        // テキストのUIを更新
        currentGems -= 3000;
        UpdateGemUI();

        //当たった10人のキャラデータを一時的に保存しておくためのリスト
        List<StateCharacter> results = new List<StateCharacter>();


        // 10回繰り返す
        for (int i = 0; i < 10; i++)
        {
            StateCharacter picked = null;
            int chance = Random.Range(0, 100); 

            // ピックアップの確率3パーセント
            if (chance < 3 && currentPickupPool.Count > 0)
            {
                // ピックアップリストから選ぶ
                picked = currentPickupPool[Random.Range(0, currentPickupPool.Count)];
                Debug.Log($"【{i + 1}人目】 ★ピックアップ確定！★");
            }
            // 星2の確率35パーセント
            else if (chance < 38 && currentCommonPool.Count > 0)
            {
                // 雑魚リストから星2を選ぶ
                picked = GetRandomCharacterByRarity(currentCommonPool, CharacterRarity.Star2);
            }
            // 星1のキャラ確定（残り62パーセント）
            else if (currentCommonPool.Count > 0)
            {
                // 雑魚リストから星1を選ぶ
                picked = GetRandomCharacterByRarity(currentCommonPool, CharacterRarity.Star1);
            }

            // 選ばれたキャラをリストに追加して、ログを出す
            if (picked != null)
            {
                results.Add(picked);
                Debug.Log($"【{i + 1}人目】 ガチャ結果: {picked.characterName}");
            }
        }
    }
    /// <summary>
    /// 特定のリストの中から、指定したレアリティのキャラだけをランダムに1人選ぶ魔法の関数
    /// </summary>
    private StateCharacter GetRandomCharacterByRarity(List<StateCharacter> pool, CharacterRarity targetRarity)
    {
        // 指定されたレアリティのキャラだけを抽出する
        List<StateCharacter> candidates = pool.FindAll(c => c.rarity == targetRarity);

        // もし候補がいたら、その中からランダムに選ぶ
        if (candidates.Count > 0)
        {
            return candidates[Random.Range(0, candidates.Count)];
        }

        Debug.LogWarning(targetRarity + " のキャラはいません");
        return null;
    }

    /// <summary>
    /// ホーム画面からガチャ一覧を開いたときLoding処理をいれる
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public IEnumerator LoadAssetsAndSetup(CharacterGachaData data)
    {
        // スプライトをセットする
        pickupCharacterImage.sprite = data.pickupCharacterSprite;
        pickUpTitleImage.sprite = data.pickUpTitleSprite;
        miniCharacterImage.sprite = data.miniCharacterSprite;
        backgroundImage.sprite = data.backgroundSprite;
        magicCircleImage.color = data.magicCircleColor;

        // 最初だけ中身をセット
        currentPickupPool = data.pickupList;
        currentCommonPool = data.commonList;

        Debug.Log($"{data.characterName} ガチャに切り替わりました");

        // 待機
        yield return new WaitForSeconds(2.5f);

        // 読み込みが完了したので、ローディングスクリーンを非表示にする
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }

        //BGMを切り替える
        ChangeBGM(data.attribute);
    }

    /// <summary>
    /// ガチャ画面の中で別のボタンを押した時にUIを更新する関数
    /// </summary>
    public void SetCharacter(CharacterGachaData data)
    {
        if (data == null) return;

        // スプライトと色を更新
        pickupCharacterImage.sprite = data.pickupCharacterSprite;
        pickUpTitleImage.sprite = data.pickUpTitleSprite;
        miniCharacterImage.sprite = data.miniCharacterSprite;
        backgroundImage.sprite = data.backgroundSprite;
        magicCircleImage.color = data.magicCircleColor;

        //ガチャの中身を更新
        currentPickupPool = data.pickupList;
        currentCommonPool = data.commonList;

        Debug.Log($"{data.characterName} ガチャに切り替わりました");

        // BGMを切り替える
        ChangeBGM(data.attribute);
    }

    /// <summary>
    /// 属性に応じてBGMを切り替える処理
    /// </summary>
    private void ChangeBGM(CharacterAttribute newAttribute)
    {
        // もし「最初じゃなくて」かつ「前と同じ属性」なら、曲を変えない
        if (!isFirstPlay && currentAttribute == newAttribute) return;

        // 新しい属性を記憶する
        currentAttribute = newAttribute;
        isFirstPlay = false;

        // 鳴らす曲を準備する空の箱
        AudioClip clipToPlay = null;

        // 属性によって箱に入れる曲を変える
        switch (newAttribute)
        {
            case CharacterAttribute.Fire:
                clipToPlay = fireBgm;
                break;
            case CharacterAttribute.Water:
                clipToPlay = waterBgm;
                break;
            case CharacterAttribute.Wood:
                clipToPlay = woodBgm;
                break;
            case CharacterAttribute.Light:
                clipToPlay = lightBgm;
                break;
            case CharacterAttribute.Dark:
                clipToPlay = darkBgm;
                break;
        }

        // スピーカーと曲がちゃんとセットされていれば再生する
        if (bgmAudioSource != null && clipToPlay != null)
        {
            // もしすでにフェード処理が動いていたら、一度強制ストップする
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            // 新しいフェード処理をスタートし、変数に覚えておく
            fadeCoroutine = StartCoroutine(CrossFadeBGM(clipToPlay));
        }
        else
        {
            Debug.LogError("スピーカーが入ってないか、曲が入ってないです。");
        }
    }

    /// <summary>
    /// 実際に音量を徐々に上げ下げする処理
    /// </summary>
    private IEnumerator CrossFadeBGM(AudioClip nextClip)
    {
        //今の曲が鳴っているなら、徐々に音量を0にする
        if (bgmAudioSource.isPlaying)
        {
            // fadeDurationの半分の時間を使って音を消していく
            float fadeOutSpeed = maxVolume / (fadeDuration / 2.0f);

            while (bgmAudioSource.volume > 0f)
            {
                bgmAudioSource.volume -= fadeOutSpeed * Time.deltaTime;
                yield return null;
            }
            bgmAudioSource.volume = 0f;
        }

        // 曲を入れ替えて再生スタート
        bgmAudioSource.clip = nextClip;
        bgmAudioSource.Play();

        // 新しい曲の音量を、徐々にmaxVolumeまで上げる
        float fadeInSpeed = maxVolume / (fadeDuration / 2.0f);

        while (bgmAudioSource.volume < maxVolume)
        {
            bgmAudioSource.volume += fadeInSpeed * Time.deltaTime;
            yield return null;
        }

        // 最後に最大音量に合わせる
        bgmAudioSource.volume = maxVolume;
    }
}