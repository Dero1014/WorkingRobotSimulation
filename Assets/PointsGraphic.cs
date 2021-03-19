using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need to figure out how to resize texture
public class PointsGraphic : MonoBehaviour
{
    public int texSize;

    [Space(10)]
    public int userFontSize = 0;
    public Color textGUIColor;

    [Space(10)]
    [Tooltip("Angle at which the ui is turned off")]public int maxAngle;

    GUIStyle style = new GUIStyle();
    private void Start()
    {
        style.alignment = TextAnchor.LowerLeft;
    }

    private void OnGUI()
    {
        Vector3 camFor = Camera.main.transform.forward;
        Vector3 direction = (transform.position - Camera.main.transform.position).normalized;

        float angle = Vector3.Angle(direction, camFor);


        if (angle<maxAngle)
        {
            style.fontSize = userFontSize;
            style.normal.textColor = textGUIColor;

            Vector2 posUi = Camera.main.WorldToScreenPoint(transform.position);

            GUI.Box(new Rect(posUi.x - (style.fontSize / 2), -posUi.y + (style.fontSize / 2), Screen.width, Screen.height), gameObject.name, style);
        }

        
    }

}
