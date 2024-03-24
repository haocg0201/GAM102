using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillImageBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    public void changeImageColor(Color fillColor)
    {
        img.color = fillColor;
    }
}
