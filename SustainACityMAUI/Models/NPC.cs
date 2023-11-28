﻿namespace SustainACityMAUI.Models;

/// <summary> Represents a non-player character in the game. </summary>
public class NPC
{
    public string Name { get; set; }
    public string ImgPath { get; set; }
    public List<Dialogue> Dialogues { get; set; }
    public List<Quest> Quests { get; set; } = new(); // Changed to a list
}
