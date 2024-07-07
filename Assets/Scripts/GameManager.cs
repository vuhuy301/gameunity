using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Image startImage;
    public GameObject[] gameElements; // Các phần tử của game cần kích hoạt khi bắt đầu
    public static GameManager Instance;
    void Start()
    {
        // Hiển thị màn hình bắt đầu khi game khởi động
        startImage = GetComponent<Image>();

        // Vô hiệu hóa các phần tử của game
        SetGameElementsActive(false);
    }

    void Update()
    {
        // Kiểm tra nếu người dùng click chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            OnStartGameClicked();
        }
    }

    public void OnStartGameClicked()
    {
        // Ẩn màn hình bắt đầu khi người chơi click

        startImage.enabled = false;
        // Kích hoạt các phần tử của game
        SetGameElementsActive(true);
    }
    

    void SetGameElementsActive(bool isActive)
    {
        foreach (GameObject element in gameElements)
        {
            element.SetActive(isActive);
        }
    }
}
