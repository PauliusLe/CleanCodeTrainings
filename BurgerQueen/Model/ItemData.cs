using System.Collections.Generic;

namespace BurgerQueen.Model
{
    //MenuItemLogic
    public class ItemData
    {
        //array of ingredients
        public List<string> List { get; } = new List<string>();
        public bool Prepared { get; set; }
        
        //Item is done and sent to service
        public bool IsSentToService { get; set; }
        
        public virtual void GetPrerequisites()
        {
            //Getting Stuff
        }

        public virtual void Prepare()
        {
            //We should prepare this
        }

        public virtual void Send()
        {
            //Send to service
        }
    }
}
