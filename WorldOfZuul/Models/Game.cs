﻿namespace WorldOfZuul.Models;

public class Game
{
    private readonly GameState _gameState;
    private readonly Dictionary<(int, int), Room> roomMap = new();
    private readonly CommandHandler _commandHandler;

    public Game()
    {
        RoomLoader roomLoader = new(Path.Combine(AppContext.BaseDirectory, "Data//Rooms.json"));
        roomMap = roomLoader.LoadRooms();
        _gameState = new() { CurrentRoom = roomMap[(0, 0)] };
        _commandHandler = new(_gameState, roomMap);
    }

    public string ExecuteCommand(string userInput)
    {
        return _commandHandler.Handle(userInput);
    }
    public static string Logo()
    {
        return "\n\n" + @"
███████╗██╗   ██╗███████╗████████╗ █████╗ ██╗███╗   ███╗     █████╗      ██████╗██╗████████╗██╗   ██╗
██╔════╝██║   ██║██╔════╝╚══██╔══╝██╔══██╗██║██╔██╗ ██╔╝    ██╔══██╗    ██╔════╝██║╚══██╔══╝╚██╗ ██╔╝
███████╗██║   ██║███████╗   ██║   ███████║██║██║╚██╗██║     ███████║    ██║     ██║   ██║    ╚████╔╝  
╚════██║██║   ██║╚════██║   ██║   ██╔══██║██║██║ ╚████║     ██╔══██║    ██║     ██║   ██║     ╚██╔╝   
███████║╚██████╔╝███████║   ██║   ██║  ██║██║██║  ╚███║     ██║  ██║    ╚██████╗██║   ██║      ██║    
╚══════╝ ╚═════╝ ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝   ╚══╝     ╚═╝  ╚═╝     ╚═════╝╚═╝   ╚═╝      ╚═╝     " + "\n\n";
    }
    public static string Welcome()
    {
        return "Dive into a world tied to the Sustainable Development Goals (SDGs). " +
            "Discover, interact, and make choices that matter." +
            "Your actions echo in the corridors of destiny." +
            CommandHandler.Help() + "\n";
    }

}