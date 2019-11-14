using System;

namespace BOT
{
    [Serializable]
    public class ng_item_bot
    {
        public int NGItemID { get; set; }
        public string EffNo { get; set; }
        public string LotNo { get; set; }
        public string NGItem { get; set; }
        public double NGBefore { get; set; }
        public double NGAfter { get; set; }
        public double NGRate { get; set; }
        public string Remark { get; set; }
    }
}
