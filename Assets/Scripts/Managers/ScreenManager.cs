using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    int deviceWidth; 
    int deviceHeight;
    int setWidth;
    int setHeight;
    private void Start()
    {
        setWidth = 1920;
        setHeight = 1080;
    }
    private void Update()
    {
        Debug.Log("deviceWidth = " + deviceWidth);
        Debug.Log("deviceHeight = " + deviceHeight);
        SetResolution(setWidth, setHeight);
    }
    /* �ػ� �����ϴ� �Լ� */
    public void SetResolution(int setWidth, int setHeight)
    {
        deviceWidth = Screen.width; // ��� �ʺ� ����
        deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), false); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }
}