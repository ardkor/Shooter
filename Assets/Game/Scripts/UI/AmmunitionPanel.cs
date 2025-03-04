using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmunitionPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _patronsCharged;
    [SerializeField] private TMP_Text _patronsRemains;

    public void SetPatronsCharged(int patrons)
    {
        _patronsCharged.text = patrons.ToString();
    }
    public void SetPatronsRemains(int patrons)
    {
        _patronsRemains.text = patrons.ToString();
    }
}
