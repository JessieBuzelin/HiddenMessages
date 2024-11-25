using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class MenuController : MonoBehaviour
{
    public GameObject buttonOne;
    public GameObject buttonTwo; 
    public GameObject exitButton;
    public GameObject notesButton; 
    public TMP_Text itemsDisplay; 

    private NoteCollection itemInventory; // Reference to the NoteCollection

    void Start()
    {
        itemInventory = FindObjectOfType<NoteCollection>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the active state of all buttons
            bool areButtonsActive = !buttonOne.activeSelf;
            buttonOne.SetActive(areButtonsActive);
            buttonTwo.SetActive(areButtonsActive);
            exitButton.SetActive(areButtonsActive);
            notesButton.SetActive(areButtonsActive);

            // Set the cursor visibility
            Cursor.visible = areButtonsActive;
            Cursor.lockState = areButtonsActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void OnNoteClicked()
    {
       
        exitButton.SetActive(false);
        notesButton.SetActive(false);
    }

    public void DisplayItems()
    {
        itemsDisplay.text = "Collected Items:\n";
        foreach (var item in itemInventory.GetItems()) // Assuming GetItems() returns a List<string>
        {
            itemsDisplay.text += item + "\n"; // Update the UI for item list
        }
    }
}
