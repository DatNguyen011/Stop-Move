using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Character character;
    public TextMeshProUGUI textPoint;
    public TextMeshProUGUI textName;
    public Image colorImage;
    public Camera Camera => CameraFollower.Instance.gameCamera;
    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;
    private float indicatorY=3f;

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(character.indicatorHead.transform.position);
        //viewPoint = Camera.WorldToViewportPoint(character.transform.position + Vector3.up * indicatorY);
        viewPoint.x = Mathf.Clamp(viewPoint.x, 0.075f,0.925f);
        viewPoint.y = Mathf.Clamp(viewPoint.y, 0.075f,0.875f);
        GetComponent<RectTransform>().anchoredPosition = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
    }

    public void InitTarget(Color color, string name, int point)
    {
        textName.text = name;
        textPoint.text = point.ToString();
        colorImage.color = color;  
    }




    public void InitTarget(int point)
    {
        textPoint.text = point.ToString();
        indicatorY = 3f + point * 0.3f;
    }
}
