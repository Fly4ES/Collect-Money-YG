using UnityEngine;
using System.Collections.Generic;
using YG;

public class InitializeSkinButton : MonoBehaviour
{
    [SerializeField] public List<ButtonSkin> ButtonSkins = new List<ButtonSkin>();

    private void Start()
    {
        if (YG2.saves.saveSkin.Count == 0)
        {
            foreach (ButtonSkin skin in ButtonSkins)
            {
                YG2.saves.saveSkin.Add(skin.FreeSkin);
                YG2.saves.selectionSkin.Add(skin.SelectionSkin);
                YG2.SaveProgress();
            }
        }

        for (int i = 0; i < ButtonSkins.Count; i++)
            ButtonSkins[i].Initialize();
    }

    private void OnValidate()
    {
        foreach (Transform child in transform)
        {
            ButtonSkin skin = child.GetComponent<ButtonSkin>();
            if (skin != null && !ButtonSkins.Contains(skin))
            {
                ButtonSkins.Add(skin);
            }
        }
    }
}
