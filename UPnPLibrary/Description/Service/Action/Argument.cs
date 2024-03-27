using System.Collections.Generic;

namespace UPnPLibrary.Description.Service.Action
{
    public class Argument
    {
        public string Name { get; set; }

        public string Direction { get; set; }

        public string RelatedStateVariable { get; set; }

        private const string NAME = "name";
        private const string DIRECTION = "direction";
        private const string RELEATED_STATE_VARIABLE = "relatedStateVariable";

        public Argument()
        {
        }

        public Argument(string name, string direction, string relatedStateVariable)
        {
            Name = name;
            Direction = direction;
            RelatedStateVariable = relatedStateVariable;
        }

        public Argument(Dictionary<string, string> map)
        {
            if (map.ContainsKey(NAME))
            {
                Name = map[NAME];
            }

            if (map.ContainsKey(DIRECTION))
            {
                Direction = map[DIRECTION];
            }

            if (map.ContainsKey(RELEATED_STATE_VARIABLE))
            {
                RelatedStateVariable = map[RELEATED_STATE_VARIABLE];
            }
        }
    }
}
