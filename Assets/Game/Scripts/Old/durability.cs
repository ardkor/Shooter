using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class durability : MonoBehaviour
{
    private RectTransform dur_trans; 
    [SerializeField] private TMP_Text t_dur;
    void Start()
    {
        dur_trans = GetComponent<RectTransform>();
    }
    public void Durability_change(int x)
    {
        t_dur.text = x.ToString();
        x = x / 100;
        dur_trans.localScale = new Vector3(x, 1f, 1f);
    }
}
