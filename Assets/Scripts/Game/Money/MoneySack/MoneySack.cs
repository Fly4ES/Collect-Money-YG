using UnityEngine;

public class MoneySack : MonoBehaviour
{
    [SerializeField] private int _moneyAll;

    private MoneyAll moneyAll;
    private MoneySliderLevel moneySliderLevel;

    private GameManager gameManager;
    private BoxCollider2D colliderSack;

    private void Start()
    {
        colliderSack = GetComponent<BoxCollider2D>();
        moneyAll = FindObjectOfType<MoneyAll>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Money")
        {
            if (moneySliderLevel == null)
            {
                moneySliderLevel = GameObject.Find("SliderBackground").GetComponent<MoneySliderLevel>();
                moneySliderLevel._currentMoney++;
                moneySliderLevel.UpdateSlider();
                moneySliderLevel.Activate_AnimationTextPlusMoney();
            }
            else
            {
                moneySliderLevel._currentMoney++;
                moneySliderLevel.UpdateSlider();
                moneySliderLevel.Activate_AnimationTextPlusMoney();
            }
            
            _moneyAll++;
            Destroy(collision.gameObject);
            moneyAll.Money_All--;
            gameManager.CheckNextLevelAndGameOver();
        }
    }
}
