using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OnValueFloatChangedText : MonoBehaviour
{
	[SerializeField] private Slider ParentSlider;
	
    private Text ValueText;

    private void Start()
    {
        ValueText = GetComponent<Text>();
		ValueText.text = ParentSlider.value.ToString("0.00");
    }

    public void OnSliderValueChanged(float value)
    {
        ValueText.text = value.ToString("0.00");
    }
}