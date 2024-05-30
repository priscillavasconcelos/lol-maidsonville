using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private Image _roleImage;
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _characterRole;
    
    [SerializeField] private TMP_Text _messageText;

    [SerializeField] private Button _messageInteract;

    [SerializeField] private int _limitCharacters = 50;
    [SerializeField] private float _tweenDuration = 0.3f;

    private Dialogue _dialogue;

    public Action<MessageView, Dialogue> MessageClicked;

    private void OnEnable()
    {
        _messageInteract.onClick.AddListener(OpenMessage);
        
        transform.localScale = Vector3.zero;
        
        transform.DOScale(1, _tweenDuration);
    }

    private void OnDisable()
    {
        _messageInteract.onClick.RemoveAllListeners();
    }

    public void Setup(Dialogue dialogue)
    {
        _dialogue = dialogue;

        _characterImage.sprite = dialogue.Phrases[0].Character.Character.CharacterSquareSprite;
        _roleImage.sprite = dialogue.Phrases[0].Character.Character.CharacterRole.RoleSprite;
        _characterName.text = dialogue.Phrases[0].Character.Character.CharacterName;
        _characterRole.text = dialogue.Phrases[0].Character.Character.CharacterRole.RoleName;

        if (dialogue.Phrases[0].Text.Length < _limitCharacters)
        {
            _messageText.text = dialogue.Phrases[0].Text;
        }
        else
        {
            _messageText.text = dialogue.Phrases[0].Text.Substring(0, _limitCharacters) + "...";
        }
        
    }

    private void OpenMessage()
    {
        transform.DOScale(0, _tweenDuration).OnComplete(() => MessageClicked?.Invoke(this, _dialogue));
    }
}
