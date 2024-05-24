using UnityEngine;

namespace PrisVas.Maidsonville.Editor
{
    [CreateAssetMenu(fileName = "SpritesListSO", menuName = "ScriptableObjects/SpritesListSO")]
    public class SpritesListSO : ScriptableObject
    {
        public string ListName;
        public Sprite[] Sprites;
    }
}
