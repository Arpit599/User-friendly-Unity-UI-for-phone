using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

#if ((UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR)
 
    private InputField p_currentInput;
 
    private Vector3 p_originalPosition;
 
    void Start () 
    {
       p_currentInput = null;
    }
 
    void Update () 
    {
       if (p_currentInput != null)
       {
          // Check if current field stopped being focused
          if(p_currentInput.isFocused == false)
          {
             // If that is the case, restore content position
             transform.position = p_originalPosition;
             p_currentInput = null;
          } //endif
       }
       else
       {
          // Check if there is an object selected
          GameObject o = EventSystem.current.currentSelectedGameObject;
          if(o != null)
          {
             // Check if it is a focused input field
             InputField input = o.GetComponent<InputField>();
             if( (input != null) && (input.isFocused == true) )
             {
                // Take note of current position, to restore it when 
                // input field focus is lost
                p_originalPosition = transform.position;
                p_currentInput = input;
 
                // Check y-coordinate of bottom-left corner
                RectTransform inputTrans = input.transform as RectTransform;
                Vector3 botLeft = inputTrans.TransformPoint(inputTrans.rect.min);
                float delta = ( (Screen.height / 2f) - botLeft.y) + 20f;
                if(delta > 0f)
                {
                   // If it is below the middle of the screen, move it up
                   transform.Translate(0f, delta, 0f, Space.World);
                } //endif
             } //endif
          } //endif
       } //endif
    }
 
#endif
}