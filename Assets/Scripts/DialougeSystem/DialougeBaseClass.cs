using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialougeSystem
{
    public class DialougeBaseClass : MonoBehaviour
    {
        private IEnumerator WriteText(string input, Text textHolder)
        {
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}