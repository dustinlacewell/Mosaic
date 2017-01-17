namespace Mosaic.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UI_Keyboard : MonoBehaviour
    {
        private Text _title;
        private InputField _input;

        public string title {
            get { return _title.text;  }
            set { _title.text = value; }
        }

        public string input {
            get { return _input.text; }
            set { _input.text = value; }
        }

        public void ClickKey(string character)
        {
            input += character;
            Debug.Log("Clicked characer: " + character);
        }

        public void Backspace()
        {
            if (input.Length > 0)
            {
                if (_input.selectionFocusPosition != _input.selectionAnchorPosition) {
                    var lv = Mathf.Min(_input.selectionAnchorPosition, _input.selectionFocusPosition);
                    var uv = Mathf.Max(_input.selectionAnchorPosition, _input.selectionFocusPosition);
                    var first = input.Substring(0, lv);
                    var second = input.Substring(uv, _input.text.Length - uv);
                } else {
                    _input.text = _input.text.Substring(0, _input.text.Length - 1);
                }
                
            }
        }

        public void Clear() {
            input = "";
        }

        public void Enter()
        {
            Debug.Log("You've typed [" + _input.text + "]");
            Clear();
        }

        private void Start()
        {
            _input = GetComponentInChildren<InputField>();
            _title = GetComponentInChildren<Text>();

            foreach (var key in GetComponentsInChildren<Button>()) {
                key.onClick.RemoveAllListeners();
                switch (key.name.ToLower()) {
                    case "space":
                        key.onClick.AddListener(delegate { ClickKey(" "); });
                        break;
                    case "backspace":
                        key.onClick.AddListener(delegate { Backspace(); });
                        break;
                    case "enter":
                        key.onClick.AddListener(delegate { Enter(); });
                        break;
                    default:
                        key.onClick.AddListener(delegate { ClickKey(key.name); });
                        break;
                }
            }
        }
    }
}