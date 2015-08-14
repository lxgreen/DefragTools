namespace DefragEngine
{
    public class ToolBundle : DefragEngineBaseUnit
    {
        private CategoryCollection _categories;

        public CategoryCollection Categories
        {
            get
            {
                return _categories ?? (_categories = new CategoryCollection());
            }
        }

        public ToolBundle(string name) : base(name)
        {
        }
    }
}