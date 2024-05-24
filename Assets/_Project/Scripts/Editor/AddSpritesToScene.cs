using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace PrisVas.Maidsonville.Editor
{
    public class AddSpritesToScene : EditorWindow
    {
        [SerializeField] private VisualTreeAsset _uxmlFile;

        private ObjectField _objField;
        private Button _addButton;
        
        [MenuItem("PrisVas/AddSpritesToScene")]
        public static void ShowEditorWindow()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow window = GetWindow<AddSpritesToScene>();
            window.titleContent = new GUIContent("Add Sprites To Scene");
        }

        public void CreateGUI()
        {
            _uxmlFile.CloneTree(rootVisualElement);

            InitializeFields();
        }

        private void InitializeFields()
        {
            _objField = rootVisualElement.Q<ObjectField>("objectField");
            _addButton = rootVisualElement.Q<Button>("addButton");
            
            _addButton.RegisterCallback<MouseUpEvent>(evt => AddSpritesPressed());
        }

        private void AddSpritesPressed()
        {
            if (_objField.value == null) return;
            if (_objField.value is not SpritesListSO list) return;
            
            GameObject parent = new GameObject(list.ListName);
            parent.transform.position = Vector3.zero;
            
            foreach (var sprite in list.Sprites)
            {
                GameObject instance = new GameObject(sprite.name);
                SpriteRenderer spriteRenderer = instance.AddComponent<SpriteRenderer>();
                instance.transform.position = Vector3.zero;
                spriteRenderer.sprite = sprite;
                
                instance.transform.SetParent(parent.transform);
            }
        }
    }
}
