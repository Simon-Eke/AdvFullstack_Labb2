using System.Reflection.Metadata;

namespace AdvFullstack_Labb2.Helpers
{
    public static class ApiRoutes
    {
        public static class MenuItem
        {
            public const string Base = "menu-items";
            public static string GetById(int id) => $"{Base}/{id}";
        }
        public static class Booking
        {
            public const string Base = "bookings";
            public static string GetById(int id) => $"{Base}/{id}";
        }
        public static class Table
        {
            public const string Base = "tables";
            public static string GetById(int id) => $"{Base}/{id}";
        }
        public static class Customer
        {
            public const string Base = "customers";
            public static string GetById(int id) => $"{Base}/{id}";
        }
        public static class Admin
        {
            public const string Base = "admins";
            public static string GetById(int id) => $"{Base}/{id}";
        }
        public static class Auth
        {
            public const string Base = "auth/login";
        }
    }
}
