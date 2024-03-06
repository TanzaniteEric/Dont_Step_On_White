using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFarestPos : MonoBehaviour
{
    PlayerController player;
    float Highest_x = 0;
    float Highest_y = 0;
    float Lowest_x = 0;
    float Lowest_y = 0;

    public Camera MainCamera;
    public float CameraMovingSpeed = 2;
    public float CameraScaleSpeed = 2;
    public bool gameEnd = false;
    Vector3 newPos;
    float NewSize;
    int coroutineCounter = 0;

    [SerializeField]
    int bgmChangeTime;

    private Animator playerAnim;
    private AudioSource bgmPlayer;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerAnim = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (!gameEnd)
        {
            Highest_x = Mathf.Max(player.transform.position.x, Highest_x);
            Highest_y = Mathf.Max(player.transform.position.y, Highest_y);
            Lowest_x = Mathf.Min(player.transform.position.x, Lowest_x);
            Lowest_y = Mathf.Min(player.transform.position.y, Lowest_y);
        }


        if (gameEnd)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }
    }

    public void OnGameEnd()
    {
        gameEnd = true;
        float Xpos = (Highest_x + Lowest_x) / 2f;
        float Ypos = (Highest_y + Lowest_y) / 2f;
        float width_x = (Highest_x - Lowest_x) + 20;
        float width_y = (Highest_y - Lowest_y) + 12;

        if (player.PlayerHealth != 0)
        {
            StartCoroutine(ChangeEndBgm(false));
        }
        else
        {
            StartCoroutine(ChangeEndBgm(true));
        }
        
        GetComponent<Animator>().SetBool("End", true);

        newPos = new Vector3(Xpos, Ypos, MainCamera.transform.position.z);

        if (width_x / 9f >= width_y / 5f)
        {
            NewSize = width_x / 9f * 5f / 2f;
        }
        else
        {
            NewSize = width_y / 2f;
        }

        StartCoroutine(CameraMove());
    }

    private IEnumerator CameraMove()
    {
        coroutineCounter = 0;
        if (MainCamera.orthographicSize <= NewSize)
        {
            MainCamera.orthographicSize += Time.fixedDeltaTime * CameraScaleSpeed;
            coroutineCounter += 1;
        }
        if (Vector3.Distance(MainCamera.transform.position, newPos) >= 0.1)
        {
            MainCamera.transform.position += (newPos - MainCamera.transform.position).normalized * Time.deltaTime * CameraMovingSpeed;
            coroutineCounter += 1;
        }

        yield return new WaitForFixedUpdate();

        if (coroutineCounter != 0) StartCoroutine(CameraMove());
    }

    IEnumerator ChangeEndBgm(bool failed)
    {
        playerAnim.SetTrigger("fadingOut");
        yield return new WaitForSeconds(bgmChangeTime);
        bgmPlayer = Camera.main.GetComponent<AudioSource>();
        if (!failed)
        {
            bgmPlayer.clip = AudioManager.instance.EndBgm;
        }
        else
        {
            bgmPlayer.clip = AudioManager.instance.EndBgm_fail;
        }
        
        bgmPlayer.Play();
    }
}
