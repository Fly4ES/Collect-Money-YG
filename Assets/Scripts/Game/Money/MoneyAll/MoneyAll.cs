using UnityEngine;
using System.Collections.Generic;

public class MoneyAll : MonoBehaviour
{
    [SerializeField] public int Money_All;
    [SerializeField] public List<GameObject> ObjectMoneyAll;

    private void OnValidate()
    {
        ObjectMoneyAll = new List<GameObject>();

        // Заполняем список детьми
        foreach (Transform child in transform)
        {
            ObjectMoneyAll.Add(child.gameObject);
        }

        MoneyAllUpdate();
    }

    public void MoneyAllUpdate()
    {
        Money_All = transform.childCount;
    }

    public void MoneyMinus(int moneyMinus)
    {
        Money_All -= moneyMinus;

        for (int i = 0; i < moneyMinus && i < ObjectMoneyAll.Count; i++)
        {
            GameObject moneyObject = ObjectMoneyAll[0];
            ObjectMoneyAll.RemoveAt(0);
            Destroy(moneyObject);
        }
    }
}
