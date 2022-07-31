using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private int _score = 0;
    public int Score { get { return _score; } }
    [SerializeField] private int _combo = 1;
    public int Combo { get { return _combo; } }
    //[SerializeField] private float _comboMeter;
    //[SerializeField] private float _comboMeterMax = 40;

    private string _scoreString = "SCORE:\n";
    private string _comboString = "X";

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _comboText;
    //[SerializeField] private Animator _comboTextAnim;
    //[SerializeField] private AudioSource _comboUpSFX;
    //[SerializeField] private AudioSource _comboBreakSFX;

    //[SerializeField] private GameObject _scorePopup;

    //public static string scorePopupPath = "Prefabs/Score Popup";


    private void Start()
    {
        UpdateScoreText();
    }


    public void IncreaseScore(int points, Vector2 position)
    {
        int _pointWorth = points * _combo;
        _score += _pointWorth;
        UpdateScoreText();
        Debug.Log("Score Increased");

        //if (Settings.showScorePopup)
        //{
        //    InitialisePopup(_pointWorth, position);
        //}
    }

    //void InitialisePopup(float points, Vector2 position)
    //{
    //    ScorePopup _popup = _popupPool.Get();
    //    _popup.transform.position = new Vector3(position.x, position.y, -5);
    //    _popup.Points = points.ToString();
    //    _popup.Initialize(this);
    //}

    public void IncreaseCombo()
    {
        _combo += 1;
        UpdateComboText();
        Debug.Log("Combo Increased");

    }

    public void SetComboFromLoad(int combo, float meterFill)
    {
        _combo = combo;
        UpdateComboText();
    }

    public void ResetScore()
    {
        _score = 0;
    }

    public void ResetCombo()
    {
        _combo = 1;
        //_comboBreakSFX.Play();
        UpdateComboText();
        //_comboTextAnim.SetTrigger("ComboBreak");
    }

    public void ResetAll()
    {
        ResetScore();
        ResetCombo();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
            _scoreText.text = _scoreString + _score.ToString();
    }

    private void UpdateComboText()
    {
        if (_comboText != null)
            _comboText.text = _comboString + Mathf.Max(1, _combo - 1).ToString();
    }

}
