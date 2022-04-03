using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _humanHappiness = 0;
    public UnityEvent<float, float> _humanHappinessUpdated;

    [SerializeField]
    private float _alienHappiness = 0;
    public UnityEvent<float, float> _alienHappinessUpdated;

    [SerializeField]
    private float _warThreshold = -20;


    [SerializeField]
    private UnityEvent<string> _warDeclared;

    private bool _gameOver;


    private InMemoryVariableStorage _variableStorage;
    private DialogueRunner _dialogueRunner;

    private void Start()
    {
        _variableStorage = FindObjectOfType<InMemoryVariableStorage>();
        _dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    private void Update()
    {
        if (_variableStorage.TryGetValue<Single>("$humanHappiness", out var humanHappiness))
        {
            if (humanHappiness != _humanHappiness)
            {
                _humanHappinessUpdated.Invoke(humanHappiness, humanHappiness - _humanHappiness);
            }
            _humanHappiness = humanHappiness;

        }
        if (_variableStorage.TryGetValue<Single>("$alienHappiness", out var alienHappiness))
        {
            if (alienHappiness != _alienHappiness)
            {
                _alienHappinessUpdated.Invoke(alienHappiness, alienHappiness - _alienHappiness);
            }
            _alienHappiness = alienHappiness;
        }

        if ((_humanHappiness <= _warThreshold || _alienHappiness <= _warThreshold) && !_gameOver)
        {
            _gameOver = true;
            _dialogueRunner.Stop();
            if (_humanHappiness <= _warThreshold && _alienHappiness <= _warThreshold)
            {
                _dialogueRunner.StartDialogue("Ending");

            }
            else if (_humanHappiness <= _warThreshold)
            {
                _dialogueRunner.StartDialogue("HadoranDeclaresWar");
            }
            else if (_alienHappiness <= _warThreshold)
            {
                _dialogueRunner.StartDialogue("Nai'tharDeclaresWar");
            }


        }
    }

    [YarnCommand("declare_war")]
    public void DeclareWar(string initiator)
    {
        _warDeclared.Invoke(initiator);
    }

    public void LoadEpilogue()
    {
        SceneManager.LoadScene("Epilogue");
    }

}
