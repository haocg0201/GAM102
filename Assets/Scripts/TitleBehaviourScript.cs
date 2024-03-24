using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TitleBehaviourScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextUGUI; // chống quét biến trong Ram local realtime => chống hack game off => check giống hồi bị ban God's human đấy ))
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C)) 
        {
            float rdColor = Random.Range(0f, 1f);
            if (rdColor < 1)
            {
                titleTextUGUI.color = new Color(rdColor + 0.1f, rdColor - 0.1f, rdColor + 0.1f, rdColor - 0.1f);
            }
            else
            {
                titleTextUGUI.color = new Color(1, 1, 1, 1);
            }
        }
        
    }
}
