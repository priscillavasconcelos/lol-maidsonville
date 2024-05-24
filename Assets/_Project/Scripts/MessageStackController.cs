using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MessageStackController : MonoBehaviour
{
    public static MessageStackController Instance;

    [SerializeField] private Transform _messageList;
    [SerializeField] private MessageView _messagePrefab;

    private ObjectPool<MessageView> _messagePool;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        
        Instance = this;
        
        _messagePool = new ObjectPool<MessageView>(() =>
        {
            return Instantiate(_messagePrefab);
        }, message =>
        {
            message.gameObject.SetActive(true);
        }, message =>
        {
            message.gameObject.SetActive(false);
        }, message =>
        {
            Destroy(message.gameObject);
        }, false, 10, 50);
    }

    public void AddMessageToStack(Dialogue dialogue)
    {
        var message = _messagePool.Get();
        message.transform.parent = _messageList;
        message.Setup(dialogue);

        message.MessageClicked += MessageClicked;
    }

    private void MessageClicked(MessageView messageView, Dialogue dialogue)
    {
        messageView.MessageClicked -= MessageClicked;
        _messagePool.Release(messageView);
        
        DialogueView.Instance.SetDialogue(dialogue);
    }
}
