using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public int score = 0;
    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "coin") 
        {
            target.gameObject.SetActive(false);
            audioSource.Play();
            score++;
            scoreText.text = "x " + score;
        } 
    }
}
