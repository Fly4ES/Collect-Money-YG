using UnityEngine;

public class MoneyMinus : MonoBehaviour
{
    [Header("Settings Money Block Minus")]
    [Space(5)]
    [SerializeField] private int _moneyMinus;

    private MoneyAll moneyAll;

    private void Start()
    {
        moneyAll = FindObjectOfType<MoneyAll>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Money")
        {
            moneyAll.MoneyMinus(_moneyMinus);

            Destroy(gameObject);
        }
    }
}
