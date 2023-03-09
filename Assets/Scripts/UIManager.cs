using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider amountSlider;
    [SerializeField] private Text amountText;
    [SerializeField] private Toggle drawRuntimeToggle;

    private void Start()
    {
        int amount = gameManager.GetAmount();
        amountSlider.value = amount / 100;
        amountText.text = $"{amount}";
    }

    public void SliderValueChanged()
    {
        int sliderAmount = (int)amountSlider.value;
        int amount = sliderAmount * 100;
        amountText.text = $"{amount}";
        gameManager.SetAmount(amount);

        if (drawRuntimeToggle.isOn)
        {
            gameManager.ClearPoints();
            gameManager.InstantiatePoints();
        }

    }
}
