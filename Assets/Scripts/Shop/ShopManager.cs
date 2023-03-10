using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI hud;

	public bool shopOpen = false;
	public Shopkeeper shopkeeper;
	public Inventory playerInventory;
	public InventoryManager inventoryManager;

    [Header("Player Inventory UI")]
    public TMP_Text playerMoneyInventory;
	public RectTransform playerItemsInventory;


    [Header("Shop UI")]
	public GameObject shopUI;
	public TMP_Text playerMoney;
    public TMP_Text shopName;
	public TMP_Text shopMoney;
	public RectTransform playerItems;
	public RectTransform shopItems;
	public GameObject itemListingPrefab;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
		UpdateMoneyUI();
    }
    public void OpenShop(Shopkeeper keeper)
	{
		shopOpen = true;
		shopkeeper = keeper;
		shopName.text = shopkeeper.shopName;
		UpdateMoneyUI();
		ClearListings();
		LoadPlayerItems();
		LoadShopItems();
		shopUI.SetActive(true);
	}

	private void ClearListings()
	{
		foreach (RectTransform listing in playerItems.transform)
		{
			Destroy(listing.gameObject);
		}

		foreach (RectTransform listing in shopItems.transform)
		{
			Destroy(listing.gameObject);
		}
	}

	private void LoadPlayerItems()
	{
		foreach (Item item in playerInventory.items)
		{
			AddItemToList(playerItems, item, ItemListing.ListingMode.SELL);
            
        }
	}

	private void LoadPlayerItemTest()
	{
		foreach (Item item in playerInventory.items)
		{
            AddItemToList(playerItemsInventory, item, ItemListing.ListingMode.SELL);
        }
	}

	private void LoadShopItems()
	{
		foreach (Item item in shopkeeper.shopInventory.items)
		{
			AddItemToList(shopItems, item, ItemListing.ListingMode.BUY);
		}
	}

	public void AddItemToList(RectTransform list, Item item, ItemListing.ListingMode mode)
	{
		GameObject clone = Instantiate(itemListingPrefab, itemListingPrefab.transform.position, Quaternion.identity);
		ItemListing listing = clone.GetComponent<ItemListing>();
		listing.ListItem(item, mode);
		listing.shopSystem = this;
		RectTransform rect = clone.GetComponent<RectTransform>();

		rect.sizeDelta = new Vector2(512, 80);

		clone.transform.SetParent(list, false);
	}

	public void RemoveItemFromList(RectTransform list, Item item)
	{
		foreach (RectTransform listing in list.transform)
		{
			if (listing.GetComponent<ItemListing>().item.id == item.id)
			{
				Destroy(listing.gameObject);
				break;
			}
		}
	}

	public void SellToShop(Item item)
	{
		if (!shopkeeper.canSellTo) return;

		if (shopkeeper.finiteMoney)
		{
			if (shopkeeper.shopInventory.money - item.price < 0)return;
		}
		playerInventory.money += item.price;
		playerInventory.RemoveItem(item); //Vende de fato os itens para o player na loja
		inventoryManager.RemoveItem(item);
        RemoveItemFromList(playerItems, item);//Altera a UI do player
		
		if (shopkeeper.finiteItems)
		{
			shopkeeper.shopInventory.AddItem(item);
			AddItemToList(shopItems, item, ItemListing.ListingMode.BUY);
		}
		if (shopkeeper.finiteMoney)
		{
			shopkeeper.shopInventory.money -= item.price;
		}
		UpdateMoneyUI();
	}

	public void BuyFromShop(Item item)
	{
		if (playerInventory.money - item.price < 0) return;
		
		playerInventory.money -= item.price;
		if (shopkeeper.finiteItems)
		{
			shopkeeper.shopInventory.RemoveItem(item);
			RemoveItemFromList(shopItems, item);
		}
		if (shopkeeper.finiteMoney)
		{
			shopkeeper.shopInventory.money += item.price;
		}
		playerInventory.AddItem(item); //Compra de fato os itens para o player na loja
		inventoryManager.AddItem(item);

        AddItemToList(playerItems, item, ItemListing.ListingMode.SELL);//Altera a UI
		UpdateMoneyUI();
	}

	private void UpdateMoneyUI()
	{
		playerMoney.text = playerInventory.money.ToString();
		hud.text = playerInventory.money.ToString();
        if (shopkeeper.finiteMoney)
		{
			shopMoney.text = shopkeeper.shopInventory.money.ToString();
		}
		else
		{
			shopMoney.text = "∞"; 
		}
	}

    public void CloseShop()
	{
		shopkeeper = null;
		shopUI.SetActive(false);
		shopOpen = false;

    }


}
