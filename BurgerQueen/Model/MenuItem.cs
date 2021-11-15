using System.Collections.Generic;

namespace BurgerQueen.Model
{
    public class MenuItem
    {
        public List<string> Ingredients { get; } = new List<string>();
        public bool IsPrepared { get; set; }
        public bool IsSentToService { get; set; }
        
        public virtual void GetPrerequisites()
        {
            //Getting Stuff
        }

        public virtual void Prepare()
        {
            //Preparing
        }

        public virtual void SendToService()
        {
            //Send to service
        }
    }
}
