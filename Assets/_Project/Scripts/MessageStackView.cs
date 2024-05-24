using UnityEngine;

public class MessageStackView : MonoBehaviour
{
    public static MessageStackView Instance;

    [SerializeField] private GameObject _messageList;
    [SerializeField] private MessageView _messagePrefab;

    public void AddMessageToStack()
    {
        
    }
}
