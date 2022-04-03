using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{

    private TextMeshProUGUI _title;
    private TextMeshProUGUI _message;

    private void Start()
    {
        _title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        _message = transform.Find("Message").GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void WarDeclared(string initiator)
    {
        gameObject.SetActive(true);
        switch (initiator)
        {
            case "Nai'thar":
                _title.text = "The Cephalons Declare War Over Calcarus I!";
                _message.text = "Despite attempts to negotiate a peace deal over the planet Calcarus I, the cephalons have decided to fight the humans for it.";
                break;
            case "Hadoran":
                _title.text = "Earth Declares War Over Calcarus I!";
                _message.text = "Despite attempts to negotiate a peace deal over the planet Calcarus I, the humans from Earth have decided to fight over it. They believe that only they have a solid claim on the planet.";
                break;
            case "Both":
                _title.text = "Cephalons and Humans Declare War Over Calcarus I!";
                _message.text = "Both sides have decided that it is not worth negotiating over any longer.";
                break;
            default:
                throw new ArgumentException($"Initiator {initiator} is not valid");
        }

    }
}
