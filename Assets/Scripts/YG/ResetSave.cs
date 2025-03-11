using UnityEngine;
using UnityEngine.UI;
using YG;
using UnityEngine.SceneManagement;

public class ResetSave : MonoBehaviour
{
    private Button buttonResetSave;

    private void Start()
    {
        buttonResetSave = GetComponent<Button>();

        buttonResetSave.onClick.AddListener(ResetSaveProgress);
    }

    private void ResetSaveProgress()
    {
        YG2.saves.money = 0;
        YG2.saves.level = 0;
        YG2.saves.levelText = 1;
        YG2.saves.music = true;
        YG2.saves.saveSkin.Clear();
        YG2.saves.selectionSkin.Clear();
        YG2.saves.serverTime = 0;
        YG2.saves.saveReward.Clear();
        YG2.saves.saveTookTheReward.Clear();
        YG2.saves.hasRewardSaved = false;

        YG2.SaveProgress();

        SceneManager.LoadScene("Game");
    }
}
