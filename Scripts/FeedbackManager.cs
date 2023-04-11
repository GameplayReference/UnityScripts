using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to handle Feed back , UI, sound 
public class FeedbackManager : MonoBehaviour
{
    public Text middleText;// not a great name.
    private GameObject TheCanvas;


    // Start is called before the first frame update
    void Start()
    {
        TheCanvas = GameObject.Find("Canvas");
        ChangeText(middleText, "Changed text ");
        
    }

    //Basic method for changing text 
    public void ChangeText(Text textOBJ, string NewText)
    {
        textOBJ.text = NewText;
    }

    // overload of Above for changing middletext
    public void ChangeText( string NewText)//should this be called ChangeMiddleText? to avoid confusion?
     {
        middleText.text = NewText;
    }


   
}
