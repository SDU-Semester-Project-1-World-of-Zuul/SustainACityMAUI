namespace WorldOfZuul.Models
{
    #region Room Class
    /// <summary> Represents a room in the game.
    /// Each room can have multiple exits leading to other rooms. </summary>
    public class Room
    {
        #region Attributes
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }

        // Mapping directions (e.g., "north", "east") to adjacent rooms.
        public Dictionary<string, Room> Exits { get; private set; } = new();
        #endregion Room Class

        #region Constructor
        /// <summary> New room with the given short and long descriptions. </summary>
        public Room(string shortDesc, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary> Sets an exit for the current room leading to a neighboring room. </summary>
        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
        #endregion Public Methods
    }
    #endregion Room Class
}