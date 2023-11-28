using SustainACityMAUI.Models;
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
}
