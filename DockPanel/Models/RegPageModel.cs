using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DockPanel.Models
{
    class RegPageModel
    {
        private List<string> controls;
        public string GetRegStatus(ref string login, ref string pass, ref string confirmPass, ref string email)
        {
            controls = new List<string>() { login, pass, confirmPass, email};
            bool successfulReg = true;
            for (int i = 0; i<controls.Count; ++i)
            {
                if (controls[i] != "Green")
                { 
                    controls[i] = "Red";
                    successfulReg = false; 
                }
            }
            login = controls[0];
            pass = controls[1];
            confirmPass = controls[2];
            email = controls[3];
            

            if (successfulReg) return "Успешная регистрация!";
            else return "Регистрация не удалась!";
        }

    }
}
