using UnityEngine;
using YG;

public class MoneyPlus : MonoBehaviour
{
    [Header("Settings Spawn Money")]
    [Space(5)]
    [SerializeField] private GameObject[] _prefabMoney;
    [Space(5)]
    [SerializeField] private int _numberOfMoney;

    private MoneyAll moneyAll;

    private Transform pointSpawn;

    private void Start()
    {
        moneyAll = FindObjectOfType<MoneyAll>();
        pointSpawn = transform.parent.GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Money")
        {
            for (int i = 0; i < _numberOfMoney; i++)
            {
                moneyAll.Money_All++;

                for(int skin = 0; skin < YG2.saves.selectionSkin.Count; skin++)
                {
                    if (YG2.saves.selectionSkin[skin])
                    {
                        GameObject newObjectMoney = Instantiate(_prefabMoney[skin], transform.position, Quaternion.identity, pointSpawn);
                        moneyAll.ObjectMoneyAll.Add(newObjectMoney);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
