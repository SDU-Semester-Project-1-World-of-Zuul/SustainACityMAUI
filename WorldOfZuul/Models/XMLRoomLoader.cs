using System.Xml.Linq;

namespace WorldOfZuul.Models
{
    public class XMLRoomLoader
    {
        #region Fields
        private readonly string _filePath;
        #endregion

        #region Constructor
        /// <summary> Initializes a new instance of the <see cref="XMLRoomLoader"/> class. </summary>
        /// <param name="filePath">The path to the XML file containing room data.</param>
        public XMLRoomLoader(string filePath)
        {
            _filePath = filePath;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary> Reads room data from an XML file and gets room instances.
        /// Also sets up the exits between rooms based on the XML data. </summary>
        /// <returns>A dictionary of rooms keyed by their names.</returns>
        public Dictionary<string, Room> GetRooms()
        {
            var xmlDoc = XDocument.Load(_filePath);
            var rooms = xmlDoc.Descendants("room").ToDictionary(
                r => r.Element("name").Value,
                r => new Room(r.Element("shortDescription").Value, r.Element("longDescription").Value)
            );

            // Set up the exits between rooms based on the XML data.
            xmlDoc.Descendants("room").ToList().ForEach(roomElem =>
                roomElem.Descendants("exit").ToList().ForEach(exitElem =>
                    rooms[roomElem.Element("name").Value].SetExit(exitElem.Attribute("direction").Value, rooms[exitElem.Attribute("room").Value])
                )
            );

            return rooms;
        }
        #endregion Public Methods
    }
}