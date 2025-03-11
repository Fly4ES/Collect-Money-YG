using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YG.Example
{
    public class LanguageExample : MonoBehaviour
    {
        public string ru, en;

        private TMP_Text textComponent;

        private void Awake()
        {
            textComponent = GetComponent<TMP_Text>();
            SwitchLanguage(YG2.lang);
        }

        private void OnEnable()
        {
            YG2.onSwitchLang += SwitchLanguage;
        }
        private void OnDisable()
        {
            YG2.onSwitchLang -= SwitchLanguage;
        }

        public void SwitchLanguage(string lang)
        {
            switch (lang)
            {
                case "ru":
                    textComponent.text = ru;
                    break;
                default:
                    textComponent.text = en;
                    break;
            }
        }
    }
}