using UnityEngine;
using System.Collections.Generic;

public class ButtonDailyRewardAllContent : MonoBehaviour
{
    public static ButtonDailyRewardAllContent Instance;

    [SerializeField] public List<ButtonDailyReward> ButtonDailyRewards = new List<ButtonDailyReward>();

    private void Awake() => Instance = this;

    private void OnValidate()
    {
        foreach (Transform child in transform)
        {
            ButtonDailyReward buttonReward = child.GetComponent<ButtonDailyReward>();
            if (buttonReward != null && !ButtonDailyRewards.Contains(buttonReward))
            {
                ButtonDailyRewards.Add(buttonReward);
            }
        }
    }
}
