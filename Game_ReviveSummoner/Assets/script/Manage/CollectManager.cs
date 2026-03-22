using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    //どこからでもアクセス可能
    public static CollectManager Instance;

    [Header("所持しているキャラクターリスト")]
    public List<StateCharacter> ownedCharacters = new List<StateCharacter>();

    private void Awake()
    {
        //他のシーンいっても消さない
        if (Instance==null)
        {
            Instance = this;
            //他のシーンにいっても消えない
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 新しく出たキャラクターを追加
    /// </summary>
    public void AddCharacter(StateCharacter newChar)
    {
        ownedCharacters.Add(newChar);
    }
}
