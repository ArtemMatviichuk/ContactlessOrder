namespace ContactlessOrder.Common.Constants
{
    public class UserRoles
    {
        public const int AdminValue = 1;
        public const int SupportValue = 2;
        public const int CompanyValue = 3;
        public const int ClientValue = 4;
        public const int CateringValue = 5;

        public const string AdminName = "Admin";
        public const string SupportName = "Support";
        public const string CompanyName = "Company";
        public const string ClientName = "Client";
        public const string CateringName = "Catering";

        public static string GetName(int value)
        {
            switch (value)
            {
                case CateringValue:
                    return CateringName;
                case AdminValue:
                    return AdminName;
                case SupportValue:
                    return SupportName;
                case CompanyValue:
                    return CompanyName;
                case ClientValue:
                    return ClientName;
                default:
                    return string.Empty;
            }
        }
    }
}
