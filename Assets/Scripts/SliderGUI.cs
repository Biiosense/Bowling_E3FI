using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class SliderGUI : MonoBehaviour
    {
        private Text text;

        void Start()
        {
            text = GetComponentInChildren<Text>();
        }

        void Update()
        {
            text.text = Mathf.RoundToInt(gameObject.GetComponent<Slider>().value).ToString();
        }
    }
}
