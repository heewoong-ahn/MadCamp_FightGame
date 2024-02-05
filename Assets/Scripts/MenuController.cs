using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI[] menuItems;
    public GameObject indicator;
    private int selectedIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = menuItems.Length - 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= menuItems.Length) selectedIndex = 0;
        }

        UpdateIndicator();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteMenuItem();
        }
    }

    void UpdateIndicator()
    {
        RectTransform textRectTransform = menuItems[selectedIndex].GetComponent<RectTransform>();
        RectTransform indicatorRectTransform = indicator.GetComponent<RectTransform>();

        // 각 menu text 너비 계산
        float textWidth = textRectTransform.sizeDelta.x;
        // indicator와 text 사이 거리 offset, 텍스트 중앙 보정 위한 y offset
        float additionalOffsetX = 20.0f;
        float additionalOffsetY = 5.0f;
        // indicator가 위치해야 할 위치 계산
        float indicatorPositionX = textRectTransform.localPosition.x - (textWidth / 2) - indicatorRectTransform.sizeDelta.x + additionalOffsetX;
        float indicatorPositionY = textRectTransform.localPosition.y + additionalOffsetY;
        // indicator 위치 업데이트
        indicatorRectTransform.localPosition = new Vector3(indicatorPositionX, indicatorPositionY, textRectTransform.localPosition.z);
    }

    void ExecuteMenuItem()
    {
        switch (selectedIndex)
        {
            case 0: // Start
                SceneManager.LoadScene("MainScene");
                break;
            case 1: // Settings, Scene 대신 Dialog로 바꾸거나 삭제할 생각 중
                SceneManager.LoadScene("TrainingScene");
                break;
            case 2: // Exit
                Application.Quit();
                break;
        }
    }
}