using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider m_XSlider;
    [SerializeField] private Slider m_YSlider;
    [SerializeField] private Slider m_ZSlider;

    [SerializeField] private TextMeshProUGUI m_XSliderText;
    [SerializeField] private TextMeshProUGUI m_YSliderText;
    [SerializeField] private TextMeshProUGUI m_ZSliderText;
    
    
    private int m_XValue;
    private int m_YValue;
    private int m_ZValue;

    public event Action<int, int, int> fireButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        m_XSlider.onValueChanged.AddListener((v) =>
        {
            m_XSliderText.text = v.ToString();
            m_XValue = (int)v;
        });
        m_YSlider.onValueChanged.AddListener((v) =>
        {
            m_YSliderText.text = v.ToString();
            m_YValue = (int)v;
        });
        m_ZSlider.onValueChanged.AddListener((v) =>
        {
            m_ZSliderText.text = v.ToString();
            m_ZValue = (int)v;
        });
    }

    public void OnFireButtonPressed()
    {
        fireButtonPressed?.Invoke(m_XValue, m_YValue, m_ZValue);
    }

}
