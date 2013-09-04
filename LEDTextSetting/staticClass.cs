using System;
using System.Collections.Generic;
using System.Text;

namespace LEDTextSetting
{
    public class staticClass
    {
        public static string restIP = string.Empty;
        public static string restPort = string.Empty;

        public static string get_set_led_url()
        {
            return string.Format("http://{0}:{1}/index.php/LED/CommandInfo/addCommandInfo", restIP, restPort);
        }
    }
}
