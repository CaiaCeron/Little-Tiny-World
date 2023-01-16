using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject inventory;

    [SerializeField]
    private float movementSpeed = 0.0f;
    private Vector2 direction;
    

    private Rigidbody2D rb2d;
    private Animator anim;

    private bool faceNorth = false;
    private bool faceSouth = false;
    private bool faceWest = false;
    private bool faceEast = false;
    private bool isInventoryOpen = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        CharacterInput();
        OpenInventory(InventoryButton());

        //For Debug Purpose.
        BackToMainMenu();
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

    private void BackToMainMenu()
    {
        if (DialogueManager.instance.isDialogPlaying) return;

        if (Input.GetButtonDown("Cancel"))
        {
            GameDataManager.instance.SaveGame();

            SceneManager.LoadSceneAsync("MainMenu");
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

    public void CloseInventoryUI()
    {
        if (DialogueManager.instance.isDialogPlaying) return;

        if (isInventoryOpen) 
        {
            isInventoryOpen = false;
        }

    }

    public void OpenInventory(bool isOpen)
    {
        inventory.SetActive(isOpen);
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
