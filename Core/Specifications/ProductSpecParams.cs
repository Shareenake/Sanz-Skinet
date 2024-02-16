namespace Core.Specifications
{
    public class ProductSpecParam
    {
        private const int MaxPageSize=50;
        public int PageIndex{get;set;}=1;

        private int _PageSize=6;
        public int pageSize
        {
            get=>_PageSize;
            set=>_PageSize=(value>MaxPageSize)? MaxPageSize:value;

        }

        public int? BrandId{get;set;}
        public int? Typeid{get;set;}
        public string Sort{get;set;}
        private string _search;
        public string Search
        { 
            get=>_search;
            set=>_search=value.ToLower();
        }

    }
}