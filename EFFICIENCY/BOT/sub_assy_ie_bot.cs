using System;

namespace BOT
{
    [Serializable]
    public class sub_assy_ie_bot
    {
        public int SubAssyIeID { get; set; }
        public int ModelSubAssyID { get; set; }
        public string SubAssy { get; set; }
        public string Model { get; set; }
        public string EffPeriod { get; set; }
        public string EffST { get; set; }
    }
}
