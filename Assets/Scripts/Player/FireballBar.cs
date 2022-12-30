using UnityEngine;
using UnityEngine.UI;

public class FireballBar : MonoBehaviour
{
    [SerializeField] private PlayerAttack Attacking;
    [SerializeField] private Image totalfireballBar;
    [SerializeField] private Image currentfireballBar;
    [SerializeField] private int fireballDivisor;

    private void Start()
    {
        totalfireballBar.fillAmount = Attacking.currentFireballAmount / fireballDivisor;
    }

    private void Update()
    {
        currentfireballBar.fillAmount = Attacking.currentFireballAmount / 10;

        if (Attacking.currentFireballAmount == 10)
            currentfireballBar.color = Color.white;
        else
            currentfireballBar.color = new Color(1, 0, 0, 0.5f);
    }
}