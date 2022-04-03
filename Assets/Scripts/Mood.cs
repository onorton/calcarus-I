using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mood : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _text;
    private GameObject _tooltip;

    private string _mood;

    private SortedDictionary<float, string> _moods;

    private void Start()
    {
        _moods = new SortedDictionary<float, string>() { [10] = "Happy", [0] = "Neutral", [-30] = "Angry", [-10] = "Frustrated" };
        _tooltip = transform.Find("Tooltip").gameObject;
        _text = _tooltip.transform.Find("Mood").GetComponent<TextMeshProUGUI>();
        _tooltip.SetActive(false);

        UpdateMood(0f, 0f);

    }

    public void UpdateMood(float newAmount, float change)
    {
        var minThresold = _moods.Keys.Min();
        if (newAmount <= minThresold)
        {
            _mood = _moods[minThresold];
            return;
        }

        foreach (var threshold in _moods.Keys)
        {
            if (newAmount >= threshold)
            {
                _mood = _moods[threshold];
            }
        }
    }

    // Needs something with a Raycast target (e.g. an image) to work
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.transform.position = eventData.position;
        _text.text = _mood;
        _tooltip.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.SetActive(false);
    }
}
