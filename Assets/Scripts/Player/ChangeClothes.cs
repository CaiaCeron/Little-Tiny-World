using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class ChangeClothes : MonoBehaviour
{

    public string SpriteSheetName;

    private string LoadedSpriteSheetName;

    private Dictionary<string, Sprite> spriteSheet;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        LoadSpriteSheet();
    }

    private void LateUpdate()
    {
   
        if (LoadedSpriteSheetName != SpriteSheetName)
        {

            LoadSpriteSheet();
        }

   
        spriteRenderer.sprite = spriteSheet[spriteRenderer.sprite.name];
    }

    private void LoadSpriteSheet()
    {
  
        var sprites = Resources.LoadAll<Sprite>(SpriteSheetName);
        spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        LoadedSpriteSheetName = SpriteSheetName;
    }

    public void OutfitBlue()
    {
        SpriteSheetName = "player";
    }

    public void OutfitRed()
    {
        SpriteSheetName = "player2";
    }
}