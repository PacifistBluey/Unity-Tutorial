using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;

        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private float delay;
        [SerializeField] private AudioClip sound;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            StartCoroutine(WriteText(input, textHolder, delay, sound));
        }
    }
}