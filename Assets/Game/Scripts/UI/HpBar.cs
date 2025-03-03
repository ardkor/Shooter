using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HpBar : MonoBehaviour
{
    [SerializeField] private TMP_Text HP_text;

    private void UpdateHpText(int hp) {
        HP_text.text = hp.ToString();
    }

}
