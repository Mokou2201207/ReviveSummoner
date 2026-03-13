using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class TestHomeButton : MonoBehaviour
{
    public GameObject a;
    public void OnButtonClicked()
    {
        a.SetActive(true);
    }
}
