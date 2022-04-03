using TMPro;
using UnityEngine;

public class DialogueHistory : MonoBehaviour
{
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    private void Start()
    {
        _text = transform.Find("Panel/TextContainer/Text").GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateHistory(string line)
    {
        _text.text = _text.text + $"{line}\n";
    }
}
