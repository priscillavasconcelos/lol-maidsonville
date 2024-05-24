using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    public static DialogueView Instance;

    [SerializeField] private GameObject _dialogueWindow; 
    
    [SerializeField] private Image _characterImage;
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _characterRole;
    
    [SerializeField] private TMP_Text _dialogueText;

    [SerializeField] private Button _nextPhrase;
    
    [SerializeField] private float _dialogueAnimationSpeed = 15;

    private Dialogue _dialogue;

    private int _currentPhrase = 0;

    private Tween _textAnimation;

    private int _actionsCompleted;
    private bool _canContinueDialogue = true;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        
        Instance = this;
    }

    private void OnEnable()
    {
        _nextPhrase.onClick.AddListener(NextPhrase);
    }

    private void OnDisable()
    {
        _nextPhrase.onClick.RemoveAllListeners();
    }

    public void SetDialogue(Dialogue dialogue)
    {
        _dialogue = dialogue;
        _currentPhrase = 0;
        
        SetPhrase(_dialogue.Phrases[_currentPhrase]);
    }

    public void SetPhrase(Phrase phrase)
    {
        //JSONNode defs = SharedState.LanguageDefs;
        
        _characterImage.sprite = phrase.Character.Character.CharacterRoundSprite;
        _characterName.text = phrase.Character.Character.CharacterName;
        _characterRole.text = phrase.Character.Character.CharacterRole.RoleName;
    
        //_dialogueText.text = defs[phrase.Id.ToString()];
        //_dialogueText.text = phrase.Text;

        
        string tempText = "";
        _textAnimation = DOTween.To(() => tempText, x => tempText = x, phrase.Text, phrase.Text.Length/_dialogueAnimationSpeed).OnUpdate(
            () =>
            {
                _dialogueText.text = tempText;
            });
        
        _dialogueWindow.SetActive(true);
    }

    private void NextPhrase()
    {
        if (!_canContinueDialogue)
        {
            return;
        }
        
        _currentPhrase++;
        
        if (_currentPhrase < _dialogue.Phrases.Count)
        {
            foreach (var action in _dialogue.Phrases[_currentPhrase].TriggerAction)
            {
                if (_dialogue.Phrases[_currentPhrase].WaitActionsToComplete)
                {
                    action.OnActionCompleted += CheckActionsCompletion;
                }
                
                action.Initialize();
            }

            _canContinueDialogue = !_dialogue.Phrases[_currentPhrase].WaitActionsToComplete;
            
            SetPhrase(_dialogue.Phrases[_currentPhrase]);
        }
        else
        {
            _dialogueWindow.SetActive(false);
        }
    }

    private void CheckActionsCompletion(ActionSO action)
    {
        _actionsCompleted++;
        action.OnActionCompleted -= CheckActionsCompletion;
        if (_actionsCompleted != _dialogue.Phrases[_currentPhrase].TriggerAction.Count) 
            return;
        
        _actionsCompleted = 0;
        _canContinueDialogue = true;
        NextPhrase();
    }
}
