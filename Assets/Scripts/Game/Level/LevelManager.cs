using UnityEngine;
using System.Collections.Generic;
using YG;

public class LevelManager : MonoBehaviour
{
    [Header("Level GameObject All")]
    [Space(5)]
    [SerializeField] private List<GameObject> _levelAll;

    public void Initialize()
    {
        //ListLevelAllUpdate();
        if (_levelAll == null || _levelAll.Count == 0)
        {
            Debug.LogError("Уровни не найдены");
        }
        else
            ActivateLevel();
    }

    private void OnValidate()
    {
        ListLevelAllUpdate();
    }

    public void ListLevelAllUpdate()
    {
        _levelAll = new List<GameObject>();

        // Заполняем список детьми
        foreach (Transform child in transform)
        {
            _levelAll.Add(child.gameObject);
        }

        for (int i = 0; i < _levelAll.Count; i++)
        {
            _levelAll[i].SetActive(false);
        }
    }

    public void ActivateLevel()
    {
        if (YG2.saves.level >= _levelAll.Count)
        {
            YG2.saves.level = 0;
            YG2.SaveProgress();
        }

        _levelAll[YG2.saves.level].SetActive(true);
    }
}
