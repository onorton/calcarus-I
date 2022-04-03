using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountChange : MonoBehaviour
{

    private Text _amountText;
    private Image _icon;

    [SerializeField]
    private Sprite _positive;
    [SerializeField]
    private Sprite _negative;

    // Start is called before the first frame update
    private void Start()
    {
        _amountText = transform.Find("Amount Text").GetComponent<Text>();
        _amountText.CrossFadeAlpha(0.0f, 0.0f, false);
        _icon = GetComponent<Image>();
        _icon.CrossFadeAlpha(0.0f, 0.0f, false);
    }

    public void Display(float newValue, float change)
    {
        _amountText.text = $"{(change >= 0 ? "+" : "")}{change}";
        _icon.sprite = (change >= 0) ? _positive : _negative;
        _icon.CrossFadeAlpha(1.0f, 0.3f, false);
        _amountText.CrossFadeAlpha(1.0f, 0.3f, false);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        _icon.CrossFadeAlpha(0.0f, 0.3f, false);
        _amountText.CrossFadeAlpha(0.0f, 0.3f, false);
    }
}
