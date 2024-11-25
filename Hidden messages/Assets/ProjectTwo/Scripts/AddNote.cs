using UnityEngine;

public class AddNote : MonoBehaviour
{
    public string itemName;
    public string noteText; 
    private NoteCollection itemInventory; 
    private MenuController menuController; 

    void Start()
    {
        itemInventory = FindObjectOfType<NoteCollection>(); 
        menuController = FindObjectOfType<MenuController>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            itemInventory.CollectItem(itemName);
            Destroy(gameObject); 
        }
    }

    public void DisplayNoteText()
    {
        itemInventory.DisplayText(noteText); 
        menuController.OnNoteClicked(); 
    }
}
