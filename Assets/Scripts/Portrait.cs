using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn.Unity;

public class Portrait : MonoBehaviour
{
    [SerializeField]
    private string _name = "";

    private List<Sprite> _sprites;

    private Image _image;
    private GameObject _moodClickableArea;
    // Start is called before the first frame update
    private void Start()
    {
        _image = GetComponent<Image>();
        _image.CrossFadeAlpha(0.0f, 0.0f, false);
        _sprites = Resources.LoadAll<Sprite>("Sprites/UI/Portraits").Where(s => s.name.Contains(_name)).ToList();
        _moodClickableArea = transform.parent.Find("Mood Clickable Area").gameObject;
        _moodClickableArea.SetActive(false);
    }


    [YarnCommand("fade_in")]
    public void FadeIn()
    {
        _image.CrossFadeAlpha(1.0f, 0.5f, false);
        _moodClickableArea.SetActive(true);
    }

    [YarnCommand("fade_out")]
    public void FadeOut()
    {
        _image.CrossFadeAlpha(0.0f, 0.5f, false);
        _moodClickableArea.SetActive(false);
    }

    [YarnCommand("set_sprite")]
    public void SetSprite(string name)
    {
        _image.sprite = _sprites.FirstOrDefault(s => s.name.Contains(name)) ?? _sprites.First(s => s.name.Contains("neutral"));
    }

}
