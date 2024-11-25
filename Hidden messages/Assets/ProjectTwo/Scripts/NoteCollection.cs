using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using System.Collections.Generic;

public class NoteCollection : MonoBehaviour
{
    private List<string> items = new List<string>();
    public TMP_Text itemsDisplay; 
    public TMP_Text noteDisplay; 
    public GameObject buttonPrefab; 

    public void CollectItem(string item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log("Collected item: " + item);
            CreateButtonForItem(item); 
        }
        else
        {
            Debug.Log("Item already collected: " + item);
        }
    }
    public List<string> GetItems()
    {
        return items; 
    }

    private void CreateButtonForItem(string itemName)
    {
        GameObject button = Instantiate(buttonPrefab);
        button.transform.SetParent(itemsDisplay.transform.parent, false); 
        button.GetComponentInChildren<TMP_Text>().text = itemName; 

        button.GetComponent<Button>().onClick.AddListener(() =>
        {
           
            string noteText = GetNoteText(itemName);
            DisplayText(noteText);
        });
    }

    private string GetNoteText(string itemName)
    {
        // Define your note texts based on item names
        switch (itemName)
        {
            case "NoteOne":
                return "This land use to be known for its peace... Now its a land of war and suffering";
            case "Note 2":
                return "This is the text for Note 2.";
          
            default:
                return "No description available.";
        }
    }

    public void DisplayText(string noteText)
    {
        noteDisplay.text = noteText; // Update the Text UI with the note text
    }

    public void DisplayItems()
    {
        itemsDisplay.text = "Collected Items:\n";
        foreach (var item in items)
        {
            itemsDisplay.text += item + "\n"; // Update the UI for item list
        }
 
    }

    public void ClearItems()
    {
        items.Clear();
        Debug.Log("All items cleared.");
    }
}
