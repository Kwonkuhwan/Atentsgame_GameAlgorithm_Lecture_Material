using System;
using System.Collections.Generic;
using System.Text;

namespace _02_Console_Training
{
    class ClsOrk : ClsCharacter
    {
        public ClsOrk(string username, int n_hp) : base()
        {
            if (username == "")
                UserName = "오크부족장";
            else
                UserName = username;

            MaxHealthPoint = n_hp;
            HealthPoint = n_hp;

            AttackDamage = (int)(AttackDamage * (2.5f));
        }
    }
}
