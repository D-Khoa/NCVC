using System;

namespace BOT
{
    [Serializable]
    public class efficiency_bot
    {
        public int EffID { get; set; }
        public string EffNo { get; set; }
        public string Model { get; set; }
        public string SubAssy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ActDate { get; set; }
        public string Shift { get; set; }
        public string Line { get; set; }
        public string Lot { get; set; }
        public string Remark { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Dept { get; set; }
        public double TotalOpTime { get; set; }
        public double TotalOpEQty { get; set; }
        public double TotalOpETime { get; set; }
        public float OT { get; set; }
        public double AddTime { get; set; }
        public double PlanQty { get; set; }
        public double PlanTime { get; set; }
        public double PlanManPower { get; set; }
        public double Item1_1 { get; set; }
        public double Item1_2 { get; set; }
        public double TTL1 { get; set; }
        public double Item2_1 { get; set; }
        public double Item2_2 { get; set; }
        public double Item2_3 { get; set; }
        public double TTL2 { get; set; }
        public double Item3_1 { get; set; }
        public double Item3_2 { get; set; }
        public double Item3_3 { get; set; }
        public double Item3_4 { get; set; }
        public double TTL3 { get; set; }
        public double NormalTime { get; set; }
        public double OTTime { get; set; }
        public double TTLTime { get; set; }
        public int Input { get; set; }
        public int Output { get; set; }
        public int NG { get; set; }
        public double MainPowerLine { get; set; }
        public double TotalTimePlan { get; set; }
        public double Efficiency { get; set; }
        public double Productivity { get; set; }
        public int StatusID { get; set; }
        public string Check { get; set; }
        public string Approve { get; set; }
        public double ST { get; set; }
    }
}
