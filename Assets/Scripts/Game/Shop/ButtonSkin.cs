using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class ButtonSkin : MonoBehaviour
{
    [Header("Settings Skin")]
    [SerializeField] private int _index;
    [SerializeField] private int _price;
    [Space(5)]
    [Tooltip("Поставьте True если хотите сделать скин бесплатным")]
    [SerializeField] public bool FreeSkin = false;
    [Tooltip("Поставьте True если хотите выбрать скин по дефолту")]
    [SerializeField] public bool SelectionSkin = false;


    private GameObject iconSelection;
    private GameObject panelLock;

    private TMP_Text textSkinShop;
    private TMP_Text textButtonSelectionAndBuy;
    private Button buttonSkin;
    private Button buttonSelectionAndBuy;

    private Vector3 originalScaleButtonSkin;
    private Vector3 originalScaleButtonSelectionAndBuy;

    private Vector3 originalScaleTextSkinShop;

    private InitializeSkinButton initializeSkinButton;

    public void Initialize()
    {
        buttonSkin = GetComponent<Button>();
        textSkinShop = GameObject.Find("TextSkinShop").GetComponent<TMP_Text>();

        Transform parent = GameObject.Find("PanelShop").transform;
        buttonSelectionAndBuy = parent.Find("ButtonSelectionAndBuy").GetComponent<Button>();

        textButtonSelectionAndBuy = buttonSelectionAndBuy.GetComponentInChildren<TMP_Text>();

        // iconSelection and PanelLock
        iconSelection = transform.Find("IconSelection")?.gameObject;
        panelLock = transform.Find("PanelLock")?.gameObject;

        initializeSkinButton = FindObjectOfType<InitializeSkinButton>();

        #region Original Scale All

        // original scale button
        originalScaleButtonSkin = buttonSkin.transform.localScale;
        originalScaleButtonSelectionAndBuy = buttonSelectionAndBuy.transform.localScale;

        // original scale text
        originalScaleTextSkinShop = textSkinShop.transform.localScale;

        #endregion

        buttonSkin.onClick.AddListener(ButtonSkinClick);

        panelLock.SetActive(!YG2.saves.saveSkin[_index]);
        iconSelection.SetActive(YG2.saves.selectionSkin[_index]);
    }

    #region Button All

    private void ButtonSkinClick()
    {
        buttonSelectionAndBuy.gameObject.SetActive(true);
        buttonSelectionAndBuy.onClick.RemoveAllListeners();
        buttonSelectionAndBuy.onClick.AddListener(ButtonSelectionAndBuyClick);

        AnimationManager.Instance.Activate_AnimationButtonScale(buttonSkin, originalScaleButtonSkin);
        AnimationManager.Instance.Activate_AnimationScaleTextPlus(textSkinShop, originalScaleTextSkinShop);

        if (YG2.saves.saveSkin[_index])
            SelectionUpdate();
        else
            BuyUpdate();
    }

    private void ButtonSelectionAndBuyClick()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(buttonSelectionAndBuy, originalScaleButtonSelectionAndBuy);

        if (YG2.saves.saveSkin[_index])
        {
            if (!YG2.saves.selectionSkin[_index])
            {
                for (int i = 0; i < YG2.saves.selectionSkin.Count; i++)
                    YG2.saves.selectionSkin[i] = false;
                YG2.saves.selectionSkin[_index] = true;
                YG2.SaveProgress();


                for (int i = 0; i < initializeSkinButton.ButtonSkins.Count; i++)
                    initializeSkinButton.ButtonSkins[i].iconSelection.SetActive(false);
                iconSelection.SetActive(YG2.saves.selectionSkin[_index]);

                SelectionUpdate();
                Bootstrap.Instance.ShopManager.SkinUpdate();
            }
        }
        else
        {
            if(YG2.saves.money >= _price)
            {
                YG2.saves.money -= _price;
                YG2.saves.saveSkin[_index] = true;

                for (int i = 0; i < YG2.saves.selectionSkin.Count; i++)
                    YG2.saves.selectionSkin[i] = false;
                YG2.saves.selectionSkin[_index] = true;

                YG2.SaveProgress();

                panelLock.SetActive(!YG2.saves.saveSkin[_index]);

                for (int i = 0; i < initializeSkinButton.ButtonSkins.Count; i++)
                    initializeSkinButton.ButtonSkins[i].iconSelection.SetActive(false);
                iconSelection.SetActive(YG2.saves.selectionSkin[_index]);

                SelectionUpdate();
                Bootstrap.Instance.ShopManager.SkinUpdate();
                MoneyManager.Instance.TextMoneyUpdate();
            }
        }
    }

    #endregion

    #region Selection And Buy Update

    private void SelectionUpdate()
    {
        if (YG2.lang == "en")
            textSkinShop.text = "PURCHASED!";
        else if (YG2.lang == "ru")
            textSkinShop.text = "КУПЛЕН!";
        textSkinShop.color = Color.yellow;

        buttonSelectionAndBuy.GetComponent<Image>().color = Color.yellow;

        if (YG2.saves.selectionSkin[_index])
        {
            if(YG2.lang == "en")
                textButtonSelectionAndBuy.text = "Selected";
            else if(YG2.lang == "ru")
                textButtonSelectionAndBuy.text = "Выбрано";
        }    
        else
        {
            if (YG2.lang == "en")
                textButtonSelectionAndBuy.text = "Choose";
            else if (YG2.lang == "ru")
                textButtonSelectionAndBuy.text = "Выбрать";
        }

    }

    private void BuyUpdate()
    {
        if(YG2.lang == "en")
            textSkinShop.text = "BUY?";
        else if(YG2.lang == "ru")
            textSkinShop.text = "КУПИТЬ?";
        textSkinShop.color = Color.green;

        textButtonSelectionAndBuy.text = _price.ToString();

        if (YG2.saves.money >= _price)
            buttonSelectionAndBuy.GetComponent<Image>().color = Color.green;
        else
            buttonSelectionAndBuy.GetComponent<Image>().color = Color.red;
    }

    #endregion
}
