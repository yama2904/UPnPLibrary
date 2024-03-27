using System.Collections.Generic;

namespace UPnPLibrary.Description.Service.StateVariable
{
    public class AllowedValueRange
    {
        public string Minimum { get; set; }

        public string Maximum { get; set; }

        public string Step { get; set; }

        private const string MINIMUM = "minimum";
        private const string MAXIMUM = "maximum";
        private const string STEP = "step";

        public AllowedValueRange()
        {
        }

        public AllowedValueRange(string minimum, string maximum, string step)
        {
            Minimum = minimum;
            Maximum = maximum;
            Step = step;
        }

        public AllowedValueRange(Dictionary<string, string> map)
        {
            if (map.ContainsKey(MINIMUM))
            {
                Minimum = map[MINIMUM];
            }

            if (map.ContainsKey(MAXIMUM))
            {
                Maximum = map[MAXIMUM];
            }

            if (map.ContainsKey(STEP))
            {
                Step = map[STEP];
            }
        }
    }
}
