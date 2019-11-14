using System;

namespace BOT
{
    [Serializable]
    public class medical_bot
    {
        public int MedicalID { get; set; }
        public string EffNo { get; set; }
        public string OPIdNo { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string MedTime { get; set; }
    }
}
