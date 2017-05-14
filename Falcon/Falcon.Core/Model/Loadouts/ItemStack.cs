namespace Falcon.Core.Model.Loadouts
{
    public class ItemStack : Item
    {
        private int _count;

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value > 0 ? value : 0;
            }
        }
    }
}