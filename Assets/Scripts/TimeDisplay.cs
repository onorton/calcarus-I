using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class TimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    [YarnCommand("set_time")]
    public void SetTime(string time)
    {
        _text.text = time;
    }
}
