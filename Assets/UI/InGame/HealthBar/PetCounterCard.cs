using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class PetCounterCard : VisualElement
{

    public static readonly string PetCounterCardUSSClassName = "pet-counter-card";

    private readonly Companion.Type CompanionType;

    private int m_count;

    private int count
    {
        get => m_count;
        set
        {
            m_count = value;
            if (countText is not null)
            {
                countText.text = value.ToString();
            }
        }


    }

    private TextElementWithClassAndName petNameText;
    private TextElementWithClassAndName countText;



    public PetCounterCard()
    {
        AddToClassList(PetCounterCardUSSClassName);
        count = 0;
        generateVisualContent += GenerateVisualContent;
    }

    public PetCounterCard(Companion.Type type, int initialCount)
    {

        AddToClassList(PetCounterCardUSSClassName);
        CompanionType = type;
        count = initialCount;

        petNameText = new(String.Format("PetCounterCardPetName-{0}", type.ToString()), new List<string> { "pet-counter-card-pet-name" });
        countText = new(String.Format("PetCounterCardCount-{0}", type.ToString()), new List<string> { "pet-counter-card-count" });

        petNameText.text = Companion.GetCompanionTypeNameFromEnum(type);
        countText.text = initialCount.ToString();

        Add(petNameText);
        Add(countText);

        if (count <= 0)
        {
            style.display = DisplayStyle.None;
        }
        else
        {
            style.display = DisplayStyle.Flex;
        }

        generateVisualContent += GenerateVisualContent;
    }

    public void SetCount(int newValue)
    {
        Debug.Log(String.Format("Set count, {0}: {1}", CompanionType.ToString(), newValue));
        count = newValue;
        countText.text = count.ToString();
        if (count <= 0)
        {
            style.display = DisplayStyle.None;
        }
        else
        {
            style.display = DisplayStyle.Flex;
        }
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {

    }
}