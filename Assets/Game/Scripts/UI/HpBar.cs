using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private RectMask2D _hpFullness;

    private int _maxHp;
    private float _maskWidth;

    public void SetMaxHp(int maxHp) 
    {
        _maxHp = maxHp;
    }
    public void UpdateHpDisplay(int hp)
    {
        _hp.text = hp.ToString();
        _hpFullness.padding = new Vector4(0, 0, _maskWidth - Mathf.Lerp(0f, _maskWidth, (float)hp / _maxHp), 0); 
    }
    private void OnEnable()
    {
        EventBus.Instance.playerHpChanged += UpdateHpDisplay;
        _maskWidth = _hpFullness.GetComponent<RectTransform>().rect.width;
    }

    private void OnDisable()
    {
        EventBus.Instance.playerHpChanged -= UpdateHpDisplay;
    }
}
