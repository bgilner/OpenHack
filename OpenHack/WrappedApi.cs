namespace OpenHack
{
    public static class WrappedApi
    {
        private const string PRODUCT_BASE_URI = "https://serverlessohproduct.trafficmanager.net/api/";
        public static string GET_PRODUCT = $"{PRODUCT_BASE_URI}GetProduct";
        public static string GET_PRODUCTS = $"{PRODUCT_BASE_URI}GetProducts";
        public const string GET_USER = "https://serverlessohuser.trafficmanager.net/api/GetUser";
    }
}
