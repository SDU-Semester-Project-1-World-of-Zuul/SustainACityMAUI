using SustainACityMAUI.Models;
using SustainACityMAUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SustainACityMAUI.ViewModels;

public class PasswordMinigame : BaseViewModel
{

    // Fields
    private readonly Player _player;

    public PasswordMinigame(Player player)
    {
        _player = player;
    }

    public string backroundImg => "computer.jpeg";
    public string[] OSstartup = { "UNLIM99 COMPUTER BY LUCANUS PORCIUS", "DEBUG ROM V3.0, CARD: NO NAME", "BYTES FREE: 1284096", "Hello Daniel! It sure feels good to be out of that tiny memory card. Bye!", "BREAK AT 0117", "READY" };
    public string instructions => "adddddddddddddddddddddsd";
    public string instructionss()
    {
        string s = "aasdas";
        return s;
    }



    public void initConsole()
    {
        for (int i = 0; i < OSstartup.Length; i++)
        {
            Label label = new Label { Text = OSstartup[i] };
            label.FontAttributes = FontAttributes.Bold;
            
        }
    }

}
