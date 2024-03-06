using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplatterSystem.TopDown;

public class PlayerController : MonoBehaviour
{
    public RenderTexture _RenderTexture;
    public bool PlayerCanBeHit = true;
    private bool inCannotHit = false;
    public int PlayerHealth = 3;
    [SerializeField] private bool DoMove = false;
    [SerializeField] private float MovingSpeed = 3;
    [SerializeField] private Vector2 input;
    [SerializeField] private Animator playeranimator;
    [SerializeField] private PlayerFarestPos EndDetect;
    [SerializeField] private AudioSource audiosource;


    private void Start()
    {
        _RenderTexture.width = Screen.width;
        _RenderTexture.height = Screen.height;
        playeranimator = GetComponent<Animator>();
        EndDetect = FindObjectOfType<PlayerFarestPos>();

    }
    private void FixedUpdate()
    {
        if (!EndDetect.gameEnd)
        {
            Playermovement();
            if (!PlayerCanBeHit && PlayerHealth > 0 && !inCannotHit)
            {
                inCannotHit = true;
                audiosource.PlayOneShot(AudioManager.instance.PlayerInjuredSound);
                StartCoroutine(PlayerUnhurtTime());
            }
        }
        if (EndDetect.gameEnd)
        {
            PlayerCanBeHit = false;
        }

    }

    private IEnumerator PlayerUnhurtTime()
    {
        PlayerHealth -= 1;
        if (PlayerHealth <= 0) EndDetect.OnGameEnd();

        playeranimator.SetBool("IsHit", true);
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(2);

        playeranimator.SetBool("IsHit", false);
        PlayerCanBeHit = true;
        GetComponent<Collider2D>().enabled = true;
        inCannotHit = false;
    }
    private void Playermovement()
    {

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        Vector3 newPos = transform.position + new Vector3(input.x, input.y, 0) * MovingSpeed * Time.fixedDeltaTime;

        StartCoroutine(MovePlayerAccordingToColor(newPos));
    }

    public IEnumerator MovePlayerAccordingToColor(Vector3 WorldPosition)
    {
        WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
        yield return frameEnd;

        Vector3 pos = Camera.main.WorldToScreenPoint(WorldPosition);

        Texture2D tex = new Texture2D(_RenderTexture.width, _RenderTexture.height);
        RenderTexture.active = _RenderTexture;

        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        Color pixel = tex.GetPixel(x, y);
        Destroy(tex);

        //Debug.Log("current standing color is :" + pixel + "current get pixel color position is: " + new Vector2(x, y));

        if (pixel.r >= 0.95 && pixel.g >= 0.95 && pixel.b >= 0.95)
        {
            DoMove = false;
        }
        else
        {
            DoMove = true;
        }

        if (DoMove) transform.position = WorldPosition;
    }

}
