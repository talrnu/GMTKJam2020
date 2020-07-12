using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OnValueChangedText : MonoBehaviour
{
	[SerializeField] private Slider ParentSlider;
	
    private Text ValueText;

    private void Start()
    {
        ValueText = GetComponent<Text>();
		ValueText.text = ParentSlider.value.ToString("0");
    }

    public void OnSliderValueChanged(float value)
    {
        ValueText.text = value.ToString("0");
    }
}
