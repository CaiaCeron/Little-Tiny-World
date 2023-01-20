using UnityEngine;

public class PlayerController : MonoBehaviour, IDataPersistence
{

    private int money;

    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private float movementSpeed = 0.0f;
    private Vector2 direction;
    

    private Rigidbody2D rb2d;
    private Animator anim;

    private bool faceNorth = false;
    private bool faceSouth = true;
    private bool faceWest = false;
    private bool faceEast = false;
    private bool isInventoryOpen = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.instance.GetPlayerMoney(money);
    }


    void Update()
    {
        CharacterInput();
        OpenInventory(InventoryButton());
        OnpenPauseMenu();
    }


    void FixedUpdate()
    {
        if (DialogueManager.instance.isDialogPlaying) return;
        CharacterMovement();
    }

    private void CharacterInput()
    {
        if (DialogueManager.instance.isDialogPlaying) return;
     
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();
        CheckLastInputDirection();
        CharacterAnimation();
    }

    private void CharacterMovement()
    {
        rb2d.MovePosition(rb2d.position + direction * movementSpeed * Time.fixedDeltaTime);
    }

    private void CharacterAnimation()
    {
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
        anim.SetFloat("Speed", direction.sqrMagnitude);

        SetPlayerFacingDirection();
    }

    private void CheckLastInputDirection()
    {
        if (direction.x > 0)
        {  
            faceNorth = false;
            faceSouth = false;
            faceWest = false;
            faceEast = true;
        }

        if (direction.x < 0)
        {
            faceNorth = false;
            faceSouth = false;
            faceWest = true;
            faceEast = false;
        }

        if (direction.y > 0)
        {
            faceNorth = true;
            faceSouth = false;
            faceWest = false;
            faceEast = false;
        }

        if (direction.y < 0)
        {
            faceNorth = false;
            faceSouth = true;
            faceWest = false;
            faceEast = false;
        }
    }

    private void SetPlayerFacingDirection()
    {
        if (faceNorth)
        {
            anim.SetBool("FaceNorth", faceNorth);
            anim.SetBool("FaceSouth", faceSouth);
            anim.SetBool("FaceEast", faceEast);
            anim.SetBool("FaceWest", faceWest);
        }
        if (faceSouth)
        {
            anim.SetBool("FaceNorth", faceNorth);
            anim.SetBool("FaceSouth", faceSouth);
            anim.SetBool("FaceEast", faceEast);
            anim.SetBool("FaceWest", faceWest);
        }
        if (faceEast)
        {
            anim.SetBool("FaceNorth", faceNorth);
            anim.SetBool("FaceSouth", faceSouth);
            anim.SetBool("FaceEast", faceEast);
            anim.SetBool("FaceWest", faceWest);
        }
        if (faceWest)
        {
            anim.SetBool("FaceNorth", faceNorth);
            anim.SetBool("FaceSouth", faceSouth);
            anim.SetBool("FaceEast", faceEast);
            anim.SetBool("FaceWest", faceWest);
        }
    }

    private bool InventoryButton()
    {
        if (DialogueManager.instance.isDialogPlaying) return isInventoryOpen;

        if (Input.GetKeyDown("tab"))
        {   
            if (isInventoryOpen )
            {
                isInventoryOpen = false;
            }
            else
            {
                isInventoryOpen = true;
            }
        }

        return isInventoryOpen;
    }

    private void OpenInventory(bool isOpen)
    {
        inventory.SetActive(isOpen);
    }

    public void PlayFootStepsSounds()
    {
        AudioManager.instance.PlayAudioClip("FootSteps");
    }

    private void OnpenPauseMenu()
    {
        if (Input.GetButtonDown("Cancel") && GameManager.instance.isGamePaused == false)
        {
            GameManager.instance.PauseGame();
            pauseMenu.SetActive(true);
            Debug.Log("Ta vindo no open");
        }
    }
   
    public void LoadGameData(GameData data)
    {
        transform.position = data.playerPosition;
    }

    public void SaveGameData(GameData data)
    {
        data.playerPosition = transform.position;
    }


}
