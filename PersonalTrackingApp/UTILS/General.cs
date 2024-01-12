using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalTrackingApp.UTILS
{
    public class General
    {
        public static Boolean isNumber(KeyPressEventArgs eventArgs)
        {
            if (!char.IsControl(eventArgs.KeyChar) && !char.IsDigit(eventArgs.KeyChar))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
