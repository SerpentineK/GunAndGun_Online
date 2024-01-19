using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PullUpMenu : MonoBehaviour, IPointerClickHandler
{
    // ���j���[�{�̂ƃ{�^��
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject buttonGraphics;

    // �ʏ펞(�i�[��)����\�����Ɉڍs����ہA�ړ����鋗��
    [SerializeField] private float movement_x;
    [SerializeField] private float movement_y;

    public bool isHidden = true;

    public void ShowMenu()
    {
        menu.transform.Translate(movement_x, movement_y, 0);
        buttonGraphics.transform.Rotate(0, 0, 180);
        isHidden = false;
    }

    public void HideMenu() 
    {
        menu.transform.Translate(-movement_x,-movement_y,0);
        buttonGraphics.transform.Rotate(0, 0, 180);
        isHidden = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isHidden) { ShowMenu(); }
        else { HideMenu(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
