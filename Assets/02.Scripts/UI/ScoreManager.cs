using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 역할 : 점수를 관리하는 점수 관리자
public class ScoreManager : MonoBehaviour
{
    // 목표 : 적을 잡을때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성
    // - 현재 점수를 표시할 UI
    public Text scoreTextUI;
    // - 현재 점수를 기억할 변수
    private int _scoreCount = 0;


    // 최고 점수 관련 속성
    public Text BestScoreTextUI;
    public int BestScoreCount = 0;

    // 헬스, 스피드 관련 속성
    public Text HealthTextUI;
    public Text SpeedTextUI;

    // 목표 : 게임을 시작 할 때 최고 점수를 불러오고, UI에 표시하고 싶다.
    // 구현 순서:
    // 1. 게임을 시작할 때
    private void Start()
    {
        // 2. 최고 점수를 불러온다.

        BestScoreCount = PlayerPrefs.GetInt("BestScore", 0);

        // 3. UI에 표시한다.
        BestScoreTextUI.text = $"Best Score : {BestScoreCount}";
    }


    // 목표 : _score 속성에 대한 캡슐화 (Get/Set)
    public int GetScoreCount()
    {
        return _scoreCount;
    }

    public void SetScoreCount(int scorecount)
    {
        // 유효성 검사
        if (scorecount < 0)
        {
            return;
        }

        _scoreCount = scorecount;
        // 목표: 스코어를 화면에 표시한다.
        scoreTextUI.text = $"Score: {GetScoreCount()}";

        // 목표: 최고 점수를 갱신하고 UI에 표시하고 싶다.
        // 1. 만약에 현재 점수가 최고 점수보다 크다면
        if (_scoreCount > BestScoreCount)
        {
            // 2. 최고 점수를 갱신하고,
            BestScoreCount = _scoreCount;

            // 목표 : 최고 점수를 저장
            // 'PlayerPrefs' 클래스를 사용
            //  -> 데이터를 '키(Key)'와 '값(Value)' 형태로 저장하는 클래스
            //  저장 할 수 있는 데이터 타입 : int, float, string 
            // 타입별로 저장/로드가 가능한 Set/Get 메서드가 있다.
            PlayerPrefs.SetInt("BestScore", BestScoreCount);

            // 3. UI에 표시한다.
            BestScoreTextUI.text = $"Best Score: {BestScoreCount}";
        }
    }
}
