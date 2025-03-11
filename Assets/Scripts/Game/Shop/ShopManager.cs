using UnityEngine;
using YG;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    [Header("Panel Shop")]
    [SerializeField] private GameObject _panelShop;

    [Space(10)]
    [Header("Shop Button")]
    [SerializeField] private Button _buttonShopOn;
    [SerializeField] private Button _buttonShopOff;

    [Space(10)]
    [Header("Skin All")]
    [SerializeField] public Sprite[] PrefabSkin;

    [Space(10)]
    [SerializeField] private GameObject buttonSkinSelectionAndBuy;
    [SerializeField] private TMP_Text textSkinShop;

    [Space(10)]
    [Header("Animation")]
    [SerializeField] private AnimationPanel animBackgShop;
    [SerializeField] private AnimationImage animPanelShop;

    private MoneyAll moneyAll;

    private Vector3 origScaleShopOn;
    private Vector3 origScaleShopOff;

    public void Initialize()
    {
        moneyAll = FindObjectOfType<MoneyAll>();

        #region Original Scale Button All

        origScaleShopOn = _buttonShopOn.transform.localScale;
        origScaleShopOff = _buttonShopOff.transform.localScale;

        #endregion

        #region List Button All

        _buttonShopOn.onClick.AddListener(ButtonShopOn);
        _buttonShopOff.onClick.AddListener(ButtonShopOff);

        #endregion

        SkinUpdate();
    }

    private void OnValidate()
    {
        animBackgShop = _panelShop.transform.Find("Background").GetComponent<AnimationPanel>();
        animPanelShop = _panelShop.transform.Find("PanelShop").GetComponent<AnimationImage>();
    }

    private void ButtonShopOn()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonShopOn, origScaleShopOn);
        _panelShop.SetActive(true);

        buttonSkinSelectionAndBuy.SetActive(false);
        if(YG2.lang == "en")
            textSkinShop.text = "Select Skin...";
        else if(YG2.lang == "ru")
            textSkinShop.text = "Выбрать скин...";
        textSkinShop.color = new Color32(0, 255, 214, 255);

        animPanelShop.AnimationImageUpdate();
        animBackgShop.AnimationPanelUpdate(0.5f, 0.85f, 0);
    }

    private void ButtonShopOff()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonShopOff, origScaleShopOff);

        StartCoroutine(TimePanelShopOff());
    }

    IEnumerator TimePanelShopOff()
    {
        yield return new WaitForSeconds(0.1f);
        _panelShop.SetActive(false);
    }

    public void SkinUpdate()
    {
        for(int i = 0; i < moneyAll.ObjectMoneyAll.Count; i++)
        {
            for (int indexSkin = 0; indexSkin < YG2.saves.selectionSkin.Count; indexSkin++)
            {
                if (YG2.saves.selectionSkin[indexSkin])
                    moneyAll.ObjectMoneyAll[i].GetComponent<Image>().sprite = PrefabSkin[indexSkin];
            }
        }
    }
}
