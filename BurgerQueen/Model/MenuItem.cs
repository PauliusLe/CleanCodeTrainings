using System.Collections.Generic;

namespace BurgerQueen.Model
{
    public class MenuItem
    {   
        public List<string> Ingredients { get; } = new List<string>();
        public bool IsPrepared { get; set; }
        public bool IsSentToService { get; set; }

        protected void Prepare()
        {
            IsPrepared = true;
        }
        
        public virtual MenuItem SendToService()
        {
            IsSentToService = true;
            return this;
        }
    }
}
