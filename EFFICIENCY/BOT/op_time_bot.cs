using System;

namespace BOT
{
    [Serializable]
    public class op_time_bot
    {
        public int OPTimeID { get; set; }
        public string EffNo { get; set; }
        public int OPQty { get; set; }
        public int OPTime { get; set; }
        public int OPQtyE { get; set; }
        public int OPTimeE { get; set; }
        public string NoiDung { get; set; }
    }
}
