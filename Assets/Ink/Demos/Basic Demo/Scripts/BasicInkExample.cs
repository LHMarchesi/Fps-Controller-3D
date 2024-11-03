using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {
    public static event Action<Story> OnCreateStory;
	
    void Awake () {
		// Remove the default message
		RemoveChildren();
		StartStory();
	}

	// Creates a new Story object with the compiled story which we can then play!
	void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				GameObject choiceButton = CreateChoiceView (choice.text.Trim ());

                Option option = choiceButton.GetComponent<Option>();
                if (option != null)
                {
                    // Tell the button what to do when we press it
                    option.onClick.AddListener(() => OnClickChoiceButton(choice));
                }
            }
		}
		// If we've read all the content and there's no choices, the story is finished!
		else {
			GameObject choice = CreateChoiceView("End of story.\nRestart?");
            Option option = choice.GetComponent<Option>();
            if (option != null)
            {
                option.onClick.AddListener(StartStory); 
            }
        }
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView(string text) {
		GameObject prefab = Instantiate(textPrefab);
		prefab.transform.SetParent(background.transform, false);
		TextMeshPro storyText = prefab.GetComponentInChildren<TextMeshPro>();

		storyText.text = text;
		storyText.transform.SetParent(background.transform, false);

		RectTransform rectTransform = storyText.GetComponent<RectTransform>();
		rectTransform.localPosition = background.transform.position;
    }

    // Creates a button showing the choice text
    GameObject CreateChoiceView (string text) {
        // Creates the button from a prefab
        GameObject choice = Instantiate (optionPrefab) as GameObject;
        choice.transform.SetParent (background.transform, false);

        // Gets the text from the button prefab
        TextMeshPro choiceText = choice.GetComponentInChildren<TextMeshPro> ();
		choiceText.text = text;

        // Ajusta RectTransform para asegurar alineación (opcional)
        RectTransform rectTransform = choice.GetComponent<RectTransform>();
		rectTransform.localPosition = background.transform.position;

        return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = background.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (background.transform.GetChild (i).gameObject);
		}
	}

	[SerializeField]
	private TextAsset inkJSONAsset = null;
	public Story story;

	[SerializeField]
	private GameObject background = null;

	// UI Prefabs
	[SerializeField]
	private GameObject textPrefab = null;
	[SerializeField]
	private GameObject optionPrefab = null;
}
