using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMaxLevel : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Text>().text = Levels.Max.ToString();
    }
}
