using System;

namespace BOT
{
    [Serializable]
    public class model_line_bot
    {
        public int ModelLineID { get; set; }
        public string ModelNo { get; set; }
        public string Line { get; set; }
        public int ModelSubID { get; set; }
        public string SubAssyNo { get; set; }
        public string EffPrefix { get; set; }
        public string Place { get; set; }
    }
}
