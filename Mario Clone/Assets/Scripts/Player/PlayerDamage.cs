using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public Image[] liveImages;
    private int lives = 4;
    private bool CanDamage = true;
    private float GroundPosition;
    void Start()
    {
        Time.timeScale = 1f;
        GroundPosition = transform.position.y-1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= GroundPosition)
        {
            StartCoroutine(RestartGame());
        }
    }

    public void DealDamage()
    {
        if (CanDamage)
        {
            if (lives > 1)
            {
                CanDamage = false;
                StartCoroutine(DamageTime());
                lives--;
                liveImages[lives].gameObject.SetActive(false);
                 
            }
            else
            {
                StartCoroutine(RestartGame());
            }

        }
    }

    IEnumerator DamageTime()
    {
        yield return new WaitForSeconds(2f);
        CanDamage = true;
    }

    public IEnumerator RestartGame()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(0);
    }
}
