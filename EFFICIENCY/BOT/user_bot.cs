using System;

namespace BOT
{
    [Serializable]
    public class user_bot
    {
        public int user_id { get; set; }
        public string user_cd { get; set; }
        public string user_name { get; set; }
        public string pass { get; set; }
        public string oldpass { get; set; }
        public bool admin_flag { get; set; }
        public string permission { get; set; }
        public string place { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static readonly user_bot userData = new user_bot();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static user_bot GetUserData()
        {
            return userData;
        }
    }
}
