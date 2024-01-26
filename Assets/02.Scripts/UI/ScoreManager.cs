using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ���� : ������ �����ϴ� ���� ������
public class ScoreManager : MonoBehaviour
{
   // ��ǥ : ���� ���������� ������ �ø���, ���� ������ UI�� ǥ���ϰ� �ʹ�.
   // �ʿ� �Ӽ�
   // - ���� ������ ǥ���� UI
    public Text scoreTextUI;
   // - ���� ������ ����� ����
    public int scoreCount = 0;

    // �ְ� ���� ���� �Ӽ�
    public Text BestScoreTextUI;
    public int BestScoreCount = 0;

    // �ｺ, ���ǵ� ���� �Ӽ�
    public Text HealthTextUI;
    public Text SpeedTextUI;

    // ��ǥ : ������ ���� �� �� �ְ� ������ �ҷ�����, UI�� ǥ���ϰ� �ʹ�.
    // ���� ����:
    // 1. ������ ������ ��
    private void Start()
    {
        // 2. �ְ� ������ �ҷ��´�.

        BestScoreCount = PlayerPrefs.GetInt("BestScore", 0);

        // 3. UI�� ǥ���Ѵ�.
        BestScoreTextUI.text = $"Best Score : {BestScoreCount}";
    }



}
