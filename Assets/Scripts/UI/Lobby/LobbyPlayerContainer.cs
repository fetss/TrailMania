using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerContainer : MonoBehaviour
{
    [SerializeField]
    private Toggle checkMark;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Image background;

    public void SetData(string name, bool ready)
    {
        if (string.IsNullOrEmpty(name))
        {
            name = "UnNamed";
        }
        nameText.text = name;
        checkMark.isOn = ready;
    }

    public void SetOwner(bool value)
    {
        if (value)
        {
            background.color = new Color(0.5f, 1, 0.5f, 1);
        }
        else
        {
            background.color = Color.white;
        }
    }

}
