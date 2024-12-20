﻿using System;
using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BubbleChatManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    TextAsset inkJSONAsset = null;

    public Story story;

    [SerializeField]
    private GameObject background = null;

    // UI Prefabs
    [SerializeField]
    private TextMeshProUGUI textPrefab = null;
    [SerializeField]
    private GameObject optionPrefab = null;

    [SerializeField]
    private GameObject choiceContainer = null;

    [SerializeField]
    private GameObject textContainer = null;


    private GameObject rewardItem;
    private Transform itemSpawnPosition;

    public void LoadChat()
    {
        // Remove the default message
        RemoveChildren();
        StartStory();
    }

    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        story.BindExternalFunction("WrongAnswer", () => WrongAnswerHandler());
        story.BindExternalFunction("GoodAnswer", () => GoodAnswerHandler());

        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    public void WrongAnswerHandler()
    {
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.GetDamage();
    }

    public void GoodAnswerHandler()
    {
        if (rewardItem != null && itemSpawnPosition != null)
        {
            Instantiate(rewardItem, itemSpawnPosition.position, Quaternion.identity);
        }
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();
        background.SetActive(true);

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                GameObject choiceButton = CreateChoiceView(choice.text.Trim());

                Option option = choiceButton.GetComponent<Option>();
                if (option != null)
                {
                    option.OnClick += () => OnClickChoiceButton(choice);
                }
            }
        }
        else // If we've read all the content and there's no choices, the story is finished!
        {
            RemoveChildren();
            background.SetActive(false);
        }

    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a textbox showing the the line of text
    void CreateContentView(string text)
    {
        TextMeshProUGUI storyText = Instantiate(textPrefab);
        storyText.gameObject.transform.SetParent(textContainer.transform, false);

        storyText.text = text;
    }

    // Creates a button showing the choice text
    GameObject CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        GameObject choice = Instantiate(optionPrefab);
        choice.transform.SetParent(choiceContainer.transform, false);

        // Gets the text from the button prefab
        TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        // Eliminar todos los hijos de textContainer
        int textChildCount = textContainer.transform.childCount;
        for (int i = textChildCount - 1; i >= 0; --i)
        {
            Destroy(textContainer.transform.GetChild(i).gameObject);
        }

        // Eliminar todos los hijos de choiceContainer
        int choiceChildCount = choiceContainer.transform.childCount;
        for (int i = choiceChildCount - 1; i >= 0; --i)
        {
            Destroy(choiceContainer.transform.GetChild(i).gameObject);
        }
    }

    public void SetJsonFile(TextAsset inkJSONAsset)
    {
        this.inkJSONAsset = inkJSONAsset;
    }

    public void FinishStory()
    {
        RemoveChildren();
        background.SetActive(false);
    }

    public void SetRewardItem(GameObject rewardItem, Transform itemSpawnPosition)
    {
            this.rewardItem = rewardItem;
            this.itemSpawnPosition = itemSpawnPosition;
    }

}
